const MongoClient = require('mongodb').MongoClient;
const { connectionString } = require(`../config/config.${process.env.NODE_ENV}.json`);

let connection;
const dbName = 'order_db'

module.exports = async () => {
    if (connection) { return connection.db(dbName); }

    connection = await MongoClient.connect(connectionString, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });

    return connection.db(dbName);
};
