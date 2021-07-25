const mongoose = require("mongoose");
const Schema = require('mongoose').Schema;

module.exports = new Schema({

    auctionId: {type: mongoose.ObjectId, required: true},
    customerId: {type: mongoose.ObjectId, required: true},
    price: {type: Number, required: true}

});
