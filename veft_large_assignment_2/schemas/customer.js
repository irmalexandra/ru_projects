const Schema = require('mongoose').Schema;

module.exports = new Schema({

    name: {type: String, required: true},
    username: {type: String, required: true},
    email: {type: String, required: true},
    address: {type: String, required: true}

});
