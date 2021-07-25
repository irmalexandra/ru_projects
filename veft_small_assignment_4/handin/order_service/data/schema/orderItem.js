const Schema = require('mongoose').Schema;

module.exports = new Schema({
    description: { type: String, required: true },
    quantity: { type: Number, required: true },
    unitPrice: { type: Number, required: true },
    rowPrice: { type: Number, required: true },
    orderId: { type: Schema.ObjectId, required: true }
});
