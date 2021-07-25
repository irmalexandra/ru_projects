const amqp = require('amqplib/callback_api');
const order = require('./data/db').Order
const orderItem = require('./data/db').OrderItem

const messageBrokerInfo = {
    exchanges: {
        order: 'order_exchange'
    },
    queues: {
      orderQueue: 'order_queue'
    },
    routingKeys: {
        createOrder: 'create_order',
        orderStatus: 'order_created'
    }
};

const createMessageBrokerConnection = () => new Promise((resolve, reject) => {
  amqp.connect('amqp://localhost', (error, connection) => {
    if (error) { reject(error);}
    resolve(connection);
  });
});

const configureMessageBroker = channel => {
  const { exchanges, queues, routingKeys} = messageBrokerInfo;
  channel.assertExchange(exchanges.order, 'direct', { durable: true });
  channel.assertQueue(queues.orderQueue, { durable: true });
  channel.bindQueue(queues.orderQueue, exchanges.order, routingKeys.createOrder);
};

const createChannel = connection => new Promise((resolve, reject) => {
  connection.createChannel((error, channel) => {
    if (error) { reject(error); }
    configureMessageBroker(channel);
    resolve(channel);
  });
});


async function write_to_database(incoming_order){
    let total_price = 0;
    let row_price = 0;
    let items_array = incoming_order.items;

    let new_order = await order.create({
        "customerEmail": incoming_order["email"],
        "totalPrice": 0,
        "orderDate": Date.now()
    });
    for (let item in items_array){

        row_price = items_array[item].quantity * items_array[item].unitPrice;
        total_price += row_price;

        await orderItem.create({
            "description": items_array[item].description,
            "quantity": items_array[item].quantity,
            "unitPrice": items_array[item].unitPrice,
            "rowPrice": row_price,
            "orderId": new_order._id
        });

    }
    new_order.totalPrice = total_price;
    new_order.save();

  return new_order;
}

(async () => {
  const connection = await createMessageBrokerConnection();
  const channel = await createChannel(connection);

  const { order } = messageBrokerInfo.exchanges;
  const { orderQueue } = messageBrokerInfo.queues;
  const { orderStatus } = messageBrokerInfo.routingKeys;

  channel.consume(orderQueue, async data => {
    const data_json = JSON.parse(data.content.toString());
    const create_order_result = await write_to_database(data_json);
    console.log(`[x] Sent: ${JSON.stringify(create_order_result)}`);
    channel.publish(order, orderStatus, new Buffer.from(JSON.stringify(create_order_result)));

  }, { noAck: true });

})().catch(error => console.error(error));
