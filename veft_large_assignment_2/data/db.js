const mongoose = require('mongoose');
const artSchema = require('../schemas/art');
const artistSchema = require('../schemas/artist');
const auctionSchema = require('../schemas/auction');
const auctionBidSchema = require('../schemas/auctionBid');
const customerSchema = require('../schemas/customer');


const connection = mongoose.createConnection("mongodb+srv://loki:the_guy1@cluster0.l96sg.azure.mongodb.net/the_thing?retryWrites=true&w=majority",
    { useNewUrlParser: true, useUnifiedTopology: true },
    (error, client) => {
        if(error) {
            throw new Error(error);}

        console.log("Successfully connected to the Database.");
    });


module.exports = {
    connection,
    Art: connection.model('Art', artSchema),
    Artist: connection.model('Artist', artistSchema),
    Auction: connection.model('Auction', auctionSchema),
    AuctionBid: connection.model('AuctionBid', auctionBidSchema),
    Customer: connection.model('Customer', customerSchema)
};


/*

const MongoClient = require('mongodb').MongoClient;
const uri = "mongodb+srv://loki:the_guy1@cluster0.l96sg.azure.mongodb.net/the_thing?retryWrites=true&w=majority";
const connection = mongoose.createConnection(
    "mongodb+srv://loki:the_guy1@cluster0.l96sg.azure.mongodb.net/the_thing?retryWrites=true&w=majority",
    { useNewUrlParser: true },
    () => {
        console.log("Connected through mongoose")
    });


const client = new MongoClient(uri, { useNewUrlParser: true });
client.connect(err => {
    const artCollection = client.db("the_thing").collection("Art");
    // perform actions on the collection object
});
/*
const connection = mongoose.createConnection('mongodb+srv://loki:the_guy1@cluster0.l96sg.azure.mongodb.net/the_thing1',
    { useNewUrlParser: true });

*/
