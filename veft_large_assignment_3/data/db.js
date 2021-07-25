const mongoose = require('mongoose');
const pickupGameSchema = require('./schemas/PickupGame');

const playerSchema = require('./schemas/Player');
const uri = "mongodb+srv://admin:admin@cluster0.6f5od.mongodb.net/LA-3_Hoop_Dreams?retryWrites=true&w=majority";

const connection = mongoose.createConnection(uri, { useNewUrlParser: true, useUnifiedTopology: true },
    (error, client) => {
        if(error) {
            throw new Error(error);}
        console.log("Successfully connected to the Database.");
    });

module.exports = {
    connection,
    PickupGame: connection.model('PickupGame', pickupGameSchema),
    Player: connection.model('Player', playerSchema)
};
