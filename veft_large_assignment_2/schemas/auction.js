const mongoose = require("mongoose");
const Schema = require('mongoose').Schema;

module.exports = new Schema({

    artId: {type: mongoose.ObjectId, required: true},
    minimumPrice: {type: Number, default:1000 , required: true },
    endDate: {type: Date,  required: true},
    auctionWinner: {type: mongoose.ObjectId}

});
