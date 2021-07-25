const pickupGameData = require('../data/db').PickupGame;
const playerData = require('../data/db').Player;
const basketballFieldService = require('../services/basketballFieldService');
const errors = require("../errors");
const mongoose = require('mongoose');

const MINLENGTH = 300000;
const MAXLENGTH = 7200000;



async function createPickupGame(parent, args){
    if(!mongoose.isValidObjectId(args["input"]["hostId"])){
        throw new errors.NotValidIdError();
    }

    const host = await playerData.findOne({_id: args["input"]["hostId"], deleted: false});
    await validatePickupGame(args["input"], host);

    let newPickupGame = await pickupGameData.create(
        {
            start: args["input"]["start"]["value"],
            end: args["input"]["end"]["value"],
            basketballFieldId: args["input"]["basketballFieldId"],
            hostId: args["input"]["hostId"]
        }
    );

     newPickupGame= await addPlayerToPickupGame("", {input:
            {
                pickupGameId: newPickupGame._id,
                playerId: args["input"]["hostId"]
            }});

    return newPickupGame
}

async function validatePickupGame(input, host){
    const location = await basketballFieldService.getBasketballFieldById("", input["basketballFieldId"]);
    if (location === null) {
        throw new errors.NotFoundError()
    }
    const pickupGames = await getPickupGamesByLocationId(input["basketballFieldId"]);

    if (host === null){
        throw new errors.NotFoundError()
    }

    const start_date = new Date(input["start"]["value"]);
    const end_date = new Date(input["end"]["value"]);

    if(start_date > end_date){
        throw new errors.MixedDatesError;
    }
    if(start_date < Date.now()){
        throw new errors.TimeHasPassedError;
    }
    if(end_date - start_date <= MINLENGTH || end_date - start_date >= MAXLENGTH ){
        throw new errors.DurationNotAllowedError;
    }

    for(let game in pickupGames){
        if(end_date >= pickupGames[game].start
            && start_date <= pickupGames[game].end
            && pickupGames[game].basketballFieldId === location.id){
            throw new errors.PickupGameOverlapError;
        }
    }
    if(location["status"] === 'CLOSED'){
        throw new errors.BasketballFieldClosedError;
    }
}

async function addPlayerToPickupGame(parent, args){

    if(!mongoose.isValidObjectId(args["input"]["playerId"])){
        throw new errors.NotValidIdError();
    }

    const pickupGame = await pickupGameData.findOne({_id: args['input']["pickupGameId"], deleted: false});

    const player = await playerData.findOne({_id: args["input"]["playerId"], deleted: false});
    await validateAddPlayerToPickupGame(pickupGame, player);

    player.playedGames.push(pickupGame.id);
    player.save();

    pickupGame.registeredPlayers.push(args["input"]["playerId"]);
    pickupGame.save();
    return pickupGame
}

async function validateAddPlayerToPickupGame(pickupGame, player){



    if (player === null){
        throw new errors.NotFoundError()
    }

    if (pickupGame === null){
        throw new errors.NotFoundError()
    }
    if (pickupGame.registeredPlayers.includes(player._id)) {
        throw new errors.PlayerAlreadyRegisteredError()
    }

    let start_date = pickupGame.start;
    let end_date = pickupGame.end;

    const pickupGames = await allPickupGames();
    for(let game in pickupGames){
    if (pickupGames[game].registeredPlayers.includes(player._id)){
        if(end_date >= pickupGames[game].start && start_date <= pickupGames[game].end ){
            if (pickupGame.hostId === player._id){
                await deletePickupGame("", {id:pickupGame._id});
            }
                throw new errors.PlayerInPickupGameOverlapError();
            }
        }

    }
    const location = await basketballFieldService.getBasketballFieldById("", pickupGame["basketballFieldId"])

    if (pickupGame.registeredPlayers.length + 1 > location.capacity){
        throw new errors.PickupGameExceedMaximumError();
    }
}

async function getPlayedGames(parent){
    let playedArr = [];
    let pickupGame;
    for (let gameId in parent.playedGames) {
        pickupGame = await pickupGameData.findOne({_id: parent.playedGames[gameId], deleted: false});
        if(pickupGame !== null){
            playedArr.push(pickupGame)
        }
    }
    return playedArr
}

async function getPickupGamesByLocationId(locationId){
    return pickupGameData.find({basketballFieldId: locationId, deleted:false})
}

async function allPickupGames(){
    return pickupGameData.find({deleted: false})
}

async function getPickupGameById(parent, args){
    if(!mongoose.isValidObjectId(args["id"])){
        throw new errors.NotValidIdError();
    }

    let pickupGame = await pickupGameData.findOne({_id: args["id"], deleted: false})
    if (pickupGame === null) {
        throw new errors.NotFoundError()
    }
    return pickupGame
}

async function removePlayerFromPickupGame(parent, args){

    if(!mongoose.isValidObjectId(args["input"]["pickupGameId"])){
        throw new errors.NotValidIdError();
    }

    let pickupGame = await pickupGameData.findOne({_id: args["input"]["pickupGameId"], deleted: false});
    await validateRemovePlayerFromPickupGame(pickupGame, args["input"]["playerId"]);
    const player = await playerData.findById(args["input"]["playerId"]);
    player.playedGames.splice(player.playedGames.indexOf(args["input"]["pickupGameId"]), 1)
    pickupGame.registeredPlayers.splice(pickupGame.registeredPlayers.indexOf(args["input"]["playerId"]), 1);
    player.save();


    if(pickupGame.registeredPlayers.length === 0){
        await deletePickupGame("", {id: pickupGame._id})
    }
    else if(pickupGame.hostId == args["input"]["playerId"]){
        let allPlayers  = [];
        for (let playerId in pickupGame.registeredPlayers){
            let player = await playerData.findOne({_id: pickupGame.registeredPlayers[playerId], deleted:false});
            if (player) {
                allPlayers.push(player)
            }
        }
        allPlayers.sort((p_a, p_b) => (p_a.name > p_b.name) ? 1 : -1);
        pickupGame.hostId = allPlayers[0]._id;

    }
    pickupGame.save();
    return true
}

async function validateRemovePlayerFromPickupGame(pickupGame, playerId){
    if (pickupGame === null){
        throw new errors.NotFoundError()
    }
    if(pickupGame.start < Date.now()){
        throw new errors.TimeHasPassedError;
    }
    if (!pickupGame.registeredPlayers.includes(playerId)) {
        throw new errors.NotFoundError()
    }

}

async function deletePickupGame(parent, args){

    if(!mongoose.isValidObjectId(args["id"])){
        throw new errors.NotValidIdError();
    }
    let game = await pickupGameData.findOne({_id: args["id"], deleted:false});
    if (game === null){
        throw new errors.NotFoundError();
    }
    game.deleted = true;
    game.save();
    return true;
}


module.exports = {
    queries: {
        allPickupGames: allPickupGames,
        pickupGame: getPickupGameById
    },
    types: {

    },
    mutations: {
        createPickupGame: createPickupGame,
        addPlayerToPickupGame: addPlayerToPickupGame,
        removePlayerFromPickupGame: removePlayerFromPickupGame,
        removePickupGame: deletePickupGame
    },
    getPlayedGames: getPlayedGames,
    getPickupGamesByLocationId: getPickupGamesByLocationId,
    allPickupGames: allPickupGames,
    deletePickupGame: deletePickupGame

};


