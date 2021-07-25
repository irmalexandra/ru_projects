const playerData = require('../data/db').Player;
const pickupGameResolver = require('./pickupGameResolver');
const errors = require("../errors");
const mongoose = require('mongoose');

async function createPlayer (parent, args) {
    const player = await playerData.create(args["input"]);
    return player
}

async function updatePlayer(parent, args) {
    if(!mongoose.isValidObjectId(args.id)){
        throw new errors.NotValidIdError();
    }

    let playerID = args.id;
    let newName = args.name;

    let player = await playerData.findOne({_id: playerID, deleted:false});
    if (player === null){
        throw new errors.NotFoundError();
    }
    player.name = newName;
    await player.save();
    return player


}

async function getAllPlayers(){
    return await playerData.find({deleted: false});
}

async function getPlayerById(id){
    if(!mongoose.isValidObjectId(id)){
        throw new errors.NotValidIdError();
    }

    let player = await playerData.findOne({_id: id, deleted:false});
    if (player === null) {
        throw new errors.NotFoundError();
    }
    return player;
}

async function getRegisteredPlayers(registeredPlayers){
    let regPlayersArr = [];
    for (let playerId in registeredPlayers){
        let player = await playerData.findOne({_id: registeredPlayers[playerId], deleted:false});
        if (player) {
            regPlayersArr.push(player)
        }
    }
    return regPlayersArr
}

async function removePlayer(parent, args){

    if(!mongoose.isValidObjectId(args["id"])){
        throw new errors.NotValidIdError();
    }

    let player = await getPlayerById(args["id"]);
    if (player === null){
        throw new errors.NotFoundError()
    }
    player.deleted = true;
    player.save();

    let pickupGamesArray = await pickupGameResolver.allPickupGames();

    for (let pickupGame in pickupGamesArray){
        let hostId = pickupGamesArray[pickupGame].hostId;

        if (player.id == hostId) {

            let allPlayers = await getRegisteredPlayers(pickupGamesArray[pickupGame].registeredPlayers);

            if (allPlayers.length <= 1){
                await pickupGameResolver.deletePickupGame("", {id:pickupGamesArray[pickupGame]._id})
            }
            else {
                allPlayers.sort((p_a, p_b) => (p_a.name > p_b.name) ? 1 : -1);
                pickupGamesArray[pickupGame].hostId = allPlayers[0]._id;
                pickupGamesArray[pickupGame].save()
            }
        }
    }

    return true
}


module.exports = {
  queries: {
      allPlayers: getAllPlayers,
      player: (parent, args) => getPlayerById(args["id"]),

  },
  types: {

  },
  mutations: {
      createPlayer : createPlayer,
      updatePlayer : updatePlayer,
      removePlayer: removePlayer
  },
    getRegisteredPlayers: getRegisteredPlayers,
    getPlayerById: getPlayerById
};


