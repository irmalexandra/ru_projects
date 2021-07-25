import pika
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

exchange_name = 'flamingo_exchange'
create_order_routing_key = 'order_create_success'
create_customer_routing_key = 'customer_create_success'
order_log_queue_name = 'order_log_queue'
customer_log_queue_name = 'customer_log_queue'

def setup_queue(exchange_name, queue_name, routing_key):
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key=routing_key)

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

setup_queue(exchange_name, order_log_queue_name, create_order_routing_key)
setup_queue(exchange_name, customer_log_queue_name, create_customer_routing_key)

def send_ack(ch, delivery_tag, success):
    if success:
        ch.basic_ack(delivery_tag)

def write_to_log_file(text):
    with open('log.txt', 'a+') as f:
        f.write('%s\n' % text)

def customer_create_event(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    write_to_log_file('Customer with id: %s was created. Fields: { "name": %s, "email": %s }' % (parsed_msg['id'], parsed_msg['name'], parsed_msg['email']))
    send_ack(ch, method.delivery_tag, True)

def order_create_event(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    write_to_log_file('Order with id: %s was created. Payment method: %s' % (parsed_msg['_id'], parsed_msg['paymentType']))
    send_ack(ch, method.delivery_tag, True)

channel.basic_consume(order_log_queue_name, order_create_event)
channel.basic_consume(customer_log_queue_name, customer_create_event)

channel.start_consuming()
connection.close()
