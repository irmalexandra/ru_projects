const mongoose = require("mongoose");
const Schema = require('mongoose').Schema;
require('mongoose-type-url');

module.exports = new Schema({

    images: [{type: mongoose.SchemaTypes.Url}],
    isAuctionItem: {type: Boolean, default: false, required: true},
    title: {type: String, required: true},
    artistId: {type: mongoose.ObjectId, required: true},
    date: {type: Date, required: true , default: Date.now},
    description: String

});

