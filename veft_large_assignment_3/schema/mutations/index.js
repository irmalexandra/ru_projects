const addPlayerToPickupGame = require('./addPlayerToPickupGame');
const createPickupGame = require('./createPickupGame');
const createPlayer = require('./createPlayer');
const removePickupGame = require('./removePickupGame');
const removePlayer = require('./removePlayer');
const removePlayerFromPickupGame = require('./removePlayerFromPickupGame');
const updatePlayer = require('./updatePlayer');

module.exports = `
    type Mutation {
        ${addPlayerToPickupGame}
        ${createPickupGame}
        ${createPlayer}
        ${removePickupGame}
        ${removePlayer}
        ${removePlayerFromPickupGame}
        ${updatePlayer}
    }
`;