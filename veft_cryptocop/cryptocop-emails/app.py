import pika
import requests
import json
from time import sleep
from os import environ


def get_connection_string():
    with open('./config/mb.%s.json' % "production", 'r') as f:
        return json.load(f)


# environ.get('PYTHON_ENV')
def connect_to_mb():
    error = False
    connection_string = get_connection_string()
    while not error:
        try:
            credentials = pika.PlainCredentials(connection_string['user'], connection_string['password'])
            connection = pika.BlockingConnection(
                pika.ConnectionParameters(connection_string['host'], 5672, connection_string['virtualhost'],
                                          credentials))
            channel = connection.channel()
            return channel, connection
        except:
            sleep(5)
            continue


channel, connection = connect_to_mb()

# Configuration
exchange_name = 'crypto_exchange'
create_order_routing_key = 'create-order'
email_queue_name = 'email_queue'
order_template = '<h2>Thank you for your order. </h2><p>We hope you will enjoy this bountiful currency and please don\'t \
hesitate to purchase more.</p></p><h3>Order info</h3><table><thead><tr style="background-color: rgba(155, 155, 155, .2)">\
<th>Full Name</th><th>Street Address</th><th>City</th><th>Zip Code</th><th>Country</th><th>\
Date of order</th><th>Total Price</th></thead><tbody>{0}</tbody></table></p>\
<br><p><h3>Order items</h3><table><thead><tr style="background-color: rgba(155, 155, 155, .2)">\
<th>Product symbol</th><th>Quantity</th><th>Unit price</th><th>Total price</th></thead><tbody>{1}</tbody</table</p>'


def setup_queue(exchange_name, queue_name, routing_key):
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key=routing_key)


# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

setup_queue(exchange_name, email_queue_name, create_order_routing_key)


def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandboxd5f661edba7547e1a491f8c53333c230.mailgun.org/messages",
        auth=("api", "6ef045c7ed98abaab40b238e4cb59ef5-9b1bf5d3-52b3808e"),
        data={"from": "Mailgun Sandbox <mailgun@sandboxd5f661edba7547e1a491f8c53333c230.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})


def send_ack(ch, delivery_tag, success):
    if success:
        ch.basic_ack(delivery_tag)


def send_order_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    msg_dict = json.loads(parsed_msg)
    email = msg_dict['Email']
    msg_dict = msg_dict

    order_info_html = ''.join(
        '<tr><td>{0}</td><td>{1} {2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>' \
            .format(msg_dict['FullName'], msg_dict['StreetName'], msg_dict['HouseNumber'], msg_dict['City'],
                    msg_dict['ZipCode'],
                    msg_dict['Country'], msg_dict['OrderDate'], msg_dict['TotalPrice']))

    order_items_html = ''.join(['<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>'
                                .format(item['ProductIdentifier'], item['Quantity'], item["UnitPrice"], item['TotalPrice'])
                                for item in msg_dict['OrderItems']])

    representation = order_template.format(order_info_html, order_items_html)
    # representation = order_template % order_items_html
    response = send_simple_message(email, 'Your order has been processed successfully.', representation)
    send_ack(ch, method.delivery_tag, response.ok)


channel.basic_consume(email_queue_name,
                      send_order_email)

channel.start_consuming()
connection.close()
