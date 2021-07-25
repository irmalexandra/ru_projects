import pika
import requests
import json
from time import sleep
from os import environ

def get_connection_string():
    with open('./config/mb.%s.json' % environ.get('PYTHON_ENV'), 'r') as f:
        return json.load(f)

def connect_to_mb():
    error = False
    connection_string = get_connection_string()
    while not error:
        try:
            credentials = pika.PlainCredentials(connection_string['user'], connection_string['password'])
            connection = pika.BlockingConnection(pika.ConnectionParameters(connection_string['host'], 5672, connection_string['virtualhost'], credentials))
            channel = connection.channel()
            return channel
        except:
            sleep(5)
            continue

channel = connect_to_mb()

# Configuration
exchange_name = 'flamingo_exchange'
create_order_routing_key = 'order_create_success'
create_customer_routing_key = 'customer_create_success'
order_email_queue_name = 'order_email_queue'
customer_email_queue_name = 'customer_email_queue'
customer_template = '<h2>Your customer was created successfully!</h2><p>We are glad to see you join our platform and hope you will like it! Now you can go into our system and log in using the credentials used to create the user.</p>'
order_template = '<h2>Thank you for ordering @ Flamingo records!</h2><p>We hope you will enjoy our lovely product and don\'t hesitate to contact us if there are any questions.</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Description</th><th>Unit price</th><th>Quantity</th><th>Row price</th></tr></thead><tbody>%s</tbody></table>'

def setup_queue(exchange_name, queue_name, routing_key):
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key=routing_key)

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

setup_queue(exchange_name, order_email_queue_name, create_order_routing_key)
setup_queue(exchange_name, customer_email_queue_name, create_customer_routing_key)

def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandbox1a8535c8dafd4dbfbb332a42f270566a.mailgun.org/messages",
        auth=("api", "0b7c1053d6f153c259af918de1b3d75a-53c13666-14e5cc8f"),
        data={"from": "Mailgun Sandbox <mailgun@sandbox1a8535c8dafd4dbfbb332a42f270566a.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})

def send_ack(ch, delivery_tag, success):
    if success:
        ch.basic_ack(delivery_tag)

def send_order_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    email = parsed_msg['email']
    items = parsed_msg['items']

    items_html = ''.join([ '<tr><td>%s</td><td>%.2f</td><td>%d</td><td>%.2f</td></tr>' % (item['name'], float(item['price']), int(item['quantity']), int(item['quantity']) * float(item['price'])) for item in items ])
    representation = order_template % items_html
    response = send_simple_message(email, 'Successful order!', representation)
    send_ack(ch, method.delivery_tag, response.ok)

def send_customer_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    email = parsed_msg['email']
    response = send_simple_message(email, 'Customer created successfully!', customer_template)
    send_ack(ch, method.delivery_tag, response.ok)

channel.basic_consume(order_email_queue_name,
                      send_order_email)

channel.basic_consume(customer_email_queue_name,
                      send_customer_email)

channel.start_consuming()
connection.close()
