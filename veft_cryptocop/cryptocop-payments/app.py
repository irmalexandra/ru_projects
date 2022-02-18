import pika
import json
from cardvalidator import luhn
from time import sleep


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
payment_queue_name = 'payment_queue'


def setup_queue(exchange, queue_name, routing_key):
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange, queue=queue_name, routing_key=routing_key)


# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

setup_queue(exchange_name, payment_queue_name, create_order_routing_key)

def send_ack(ch, delivery_tag, success):
    if success:
        ch.basic_ack(delivery_tag)

def validate_payment_card(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    msg_dict = json.loads(parsed_msg)
    valid_dict = {True: "valid!", False: "not valid!"}
    valid = luhn.is_valid(msg_dict["CreditCard"])
    print('This credit card number is ' + valid_dict[valid])
    send_ack(ch, method.delivery_tag, valid)


channel.basic_consume(payment_queue_name,
                      validate_payment_card)

channel.start_consuming()
connection.close()
