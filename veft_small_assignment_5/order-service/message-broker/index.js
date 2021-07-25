const amqp = require('amqplib/callback_api');
const config = require(`../config/config.${process.env.NODE_ENV}.json`)

const createMessageBrokerConnection = host => new Promise((resolve, reject) => {
    amqp.connect(`amqp://${host}`, (err, conn) => {
        if (err) { reject(err); }
        resolve(conn);
    });
});

const createChannel = (connection, exchangeName) => new Promise((resolve, reject) => {
    connection.createChannel((err, channel) => {
        if (err) { reject(err); }
        channel.assertExchange(exchangeName, 'direct', { durable: true });
        resolve(channel);
    });
});

module.exports = {
    getChannel: async exchangeName => {
        const connection = await createMessageBrokerConnection(config.messageBrokerHost);
        return await createChannel(connection, exchangeName);
    }
};
