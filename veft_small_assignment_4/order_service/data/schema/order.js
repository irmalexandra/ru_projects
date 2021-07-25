const Schema = require('mongoose').Schema;

module.exports = new Schema({
    customerEmail: { type: String, required: true },
    totalPrice: { type: Number, required: true },
    orderDate: { type: Date, required: true }
});
