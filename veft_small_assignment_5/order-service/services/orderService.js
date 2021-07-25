const loadDb = require('../data/db');
const request = require('request-promise-native')
const ObjectID = require('mongodb').ObjectID;
const { endpoints } = require(`../config/config.${process.env.NODE_ENV}.json`)

const getCollection = async () => {
    const db = await loadDb();
    return db.collection('orders');
};

module.exports = {
    getOrdersForCustomer: async customerId => {
        const collection = await getCollection();
        const orders = await collection.find({ customerId }).toArray();
        const customerResult = await request.get(`${endpoints.customerService}/api/customers/${customerId}`);
        return orders;
    },
    createOrder: async order => {
        const orders = await getCollection();
        const { customerId } = order;
        const customerResult = await request.get(`${endpoints.customerService}/api/customers/${customerId}`);
        const customer = JSON.parse(customerResult);

        const result = await orders.insertOne(order);
        return { order: result.ops[0], email: customer.email };
    }
}
