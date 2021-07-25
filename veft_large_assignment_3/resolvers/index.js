const playerResolver = require('./playerResolver');
const basketballFieldResolver = require('./basketballFieldResolver');
const pickupGameResolver = require('./pickupGameResolver');
const moment = require('moment');
const { GraphQLScalarType } = require('graphql');

module.exports = {
  Query: {
    ...playerResolver.queries,
    ...basketballFieldResolver.queries,
    ...pickupGameResolver.queries,
  },
  Mutation: {
    ...playerResolver.mutations,
    ...basketballFieldResolver.mutations,
    ...pickupGameResolver.mutations,
  },
  Player: {
    playedGames: parent => pickupGameResolver.getPlayedGames(parent),

  },
  PickupGame: {
    host: parent => playerResolver.getPlayerById(parent.hostId),
    location: parent => basketballFieldResolver.getFieldById("" , parent.basketballFieldId),
    registeredPlayers: parent => playerResolver.getRegisteredPlayers(parent.registeredPlayers)
  },
  BasketballField: {
    pickupGames: parent => pickupGameResolver.getPickupGamesByLocationId(parent.id),
  },
  Moment: new GraphQLScalarType({
    name: "Moment",
    description: "Used to format dates correctly.",
    parseValue: (value) => {return value},
    parseLiteral: (value) => {return value},
    serialize: (value) => moment(value).format("llll")
  }),

};