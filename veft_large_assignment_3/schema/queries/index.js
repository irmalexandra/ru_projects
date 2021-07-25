const allBasketBallFields = require('./allBasketballFields');
const allPickupGames = require('./allPickupGames');
const allPlayers = require('./allPlayers');
const basketballField = require('./basketballField');
const pickupGame = require('./pickupGame');
const player = require('./player');
module.exports = `
    type Query {
        ${allBasketBallFields}
        ${allPickupGames}
        ${allPlayers}
        ${basketballField}
        ${pickupGame}
        ${player}
    }
`;
