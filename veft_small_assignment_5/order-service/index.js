const express = require('express');
const bodyParser = require('body-parser');
const orderService = require('./services/orderService');
const { getChannel } = require('./message-broker');
const { exchangeName, routingKey } = require('./config/mb.json');
const app = express();
app.use(bodyParser.json());

const port = process.env.PORT || 4000;
app.get('/api/customers/:customerId/orders', async (req, res) => {

    try {
        const orders = await orderService.getOrdersForCustomer(parseInt(req.params.customerId));
        return res.json(orders);
    } catch (err) {
        const statusCode = err.statusCode ? err.statusCode : 500;
        return res.status(statusCode).end();
    }
});

app.post('/api/orders', async (req, res) => {
    try {
        var response = await orderService.createOrder(req.body);
    } catch (err) {
        const statusCode = err.statusCode ? err.statusCode : 500;
        return res.status(statusCode).end();
    }

    const channel = await getChannel(exchangeName);
    channel.publish(exchangeName, routingKey, Buffer.from(JSON.stringify({ ...response.order, email: response.email })));
    return res.json(response.order);
});

app.listen(port, () => console.log(`Listening on port ${port}`));
