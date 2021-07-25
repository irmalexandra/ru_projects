const orderSchema = require('./schema/order');
const orderItemSchema = require('./schema/orderItem');
const mongoose = require('mongoose');
const uri = "mongodb+srv://admin:admin@cluster0.6f5od.mongodb.net/SA-4_Cactus_Heaven?retryWrites=true&w=majority";

const connection = mongoose.createConnection(uri, {useNewUrlParser: true},
    (error) => {
    if(error){
        console.log("error occurred in connecting to DB")
    }
    else{
        console.log("Connection to database successful")
    }
    });

module.exports = {
    Order: connection.model('Order', orderSchema),
    OrderItem: connection.model('OrderItem', orderItemSchema)
};
