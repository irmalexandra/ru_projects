const mongoose = require("mongoose");
const Schema = require('mongoose').Schema;

module.exports = new Schema({

    deleted: {type: Boolean, require: true, default: false},
    name: {type: String, require: true},
    playedGames: [{type: mongoose.ObjectId}]

});