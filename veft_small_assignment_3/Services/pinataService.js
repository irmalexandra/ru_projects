const getPinataModel = require("../Models/Pinata").getPinataModel;
const data = require("../Data/data.json");

const fs = require('fs');
const request = require('request');


const download = function(uri, filename, callback){
    request.head(uri, function(err, res, body){
        request(uri).pipe(fs.createWriteStream(filename)).on('close', callback);
    });
};

function isValidUrl(string) {
    try {
        new URL(string);
    } catch (_) {
        return false;
    }

    return true;
}


const PinataService = () => {
    let allPinatas = data.pinatas;

    const getAllPinatas = () => {
        let collection = [];
        for (let i = 0; i < allPinatas.length; i++) {
            if(allPinatas[i].currentHits === undefined){
                allPinatas[i].currentHits = 0;
            }
            collection[i] = {
                "id": allPinatas[i].id,
                "name": allPinatas[i].name,
                "maximumHits": allPinatas[i].maximumHits,
                "currentHits": allPinatas[i].currentHits
            }
        }
        return collection;
    };

    const getPinataById = id => {

        for (let i = 0; i < allPinatas.length; i++){
            if (allPinatas[i].id === parseInt(id)){
                if(allPinatas[i].currentHits === undefined){
                    allPinatas[i].currentHits = 0;
                }
                return allPinatas[i];
            }
        }
        return null;
    };
    const createPinata = pinata => {
        let newPinata = getPinataModel();

        newPinata.id = allPinatas.length + 1;
        newPinata.name = pinata.name;
        newPinata.maximumHits = pinata.maximumHits;
        newPinata.surprise = pinata.surprise;

        allPinatas.push(newPinata);
        return newPinata;
    };
    const hitPinata = pinata => {
        pinata.currentHits ++;
        if (pinata.currentHits < pinata.maximumHits){
            return {
                "statusCode": 204,
                "content": null
            }
        }
        else if (pinata.currentHits === pinata.maximumHits){
            if (isValidUrl(pinata.surprise)){
                download(pinata.surprise, 'images/'+pinata.name+".png", function(){
                    console.log('image saved to images/');
                });

            }
            else{
                fs.appendFile('surprise.txt', pinata.surprise+"\n", function (err) {
                    if (err) throw err;
                });
            }
            return {
                "statusCode": 200,
                "content": pinata.surprise
            }
        }
        else {
            return {
                "statusCode": 423,
                "content": "Locked"
            }
        }

    };

    return {
        getAllPinatas,
        getPinataById,
        createPinata,
        hitPinata
    }


};

module.exports = PinataService();