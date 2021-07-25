const Schema = require('mongoose').Schema;

module.exports = new Schema({

    name: {type: String, required: true},
    nickName: {type: String, required: true},
    address: {type: String, required: true},
    memberSince: {type: Date, required: true, default: Date.now},

});
