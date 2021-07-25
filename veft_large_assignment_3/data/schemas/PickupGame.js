const mongoose = require("mongoose");
const Schema = require('mongoose').Schema;

module.exports = new Schema({

    deleted: {type: Boolean, require: true, default: false},
    start: {type: Date, require: true},
    end: {type: Date, require: true},
    basketballFieldId: {type: String, require: true},
    hostId: {type: mongoose.ObjectId, require: true},
    registeredPlayers: [{type: mongoose.ObjectId}]

});
