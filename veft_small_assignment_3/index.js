const express = require('express');
const bodyParser = require('body-parser');
const port = 3000;

const candyService = require("./Services/candyService");
const pinataService = require("./Services/pinataService");
const offerService = require("./Services/offerService");

const server = express();

server.use(bodyParser.json());

server.listen(port, () => {
    console.log(`Server is listening on port: ${port}.`);
});


//====================================== GET ======================================//

server.get("/api/candies", function(request, response){
    let candies = candyService.getAllCandies();
    response.status(200);
    return response.json(candies);
});

server.get("/api/candies/:id", function(request, response){
    const id = request.params.id;
    const candy = candyService.getCandyById(id);
    if (candy){
        response.status(200);
    } else{
        response.status(404);
    }
    return response.json(candy);
});

server.get("/api/offers", function(request, response){
    let offers = offerService.getAllOffers();
    response.status(200);
    return response.json(offers);
});

server.get("/api/pinatas", function(request, response){
    let pinatas = pinataService.getAllPinatas();
    response.status(200);

    return response.json(pinatas);
});

server.get("/api/pinatas/:id", function(request, response){
    const id = request.params.id;
    const pinata = pinataService.getPinataById(id);
    if (pinata){
        response.status(200);
    } else{
        response.status(404);
    }
    let json_res = {
        "id": pinata.id,
        "name": pinata.name,
        "maximumHits": pinata.maximumHits,
        "currentHits": pinata.currentHits
    };
    return response.json(json_res);
});


//====================================== POST ======================================//

server.post("/api/candies", function(request, response){
    const candy = request.body;
    const returned_candy = candyService.createCandy(candy);
    return response.status(200).json(returned_candy);
});

server.post("/api/pinatas", function(request, response){
    const pinata = request.body;
    const returned_pinata = pinataService.createPinata(pinata);
    return response.status(200).json(returned_pinata);
});


//====================================== PATCH ======================================//

server.patch("/api/pinatas/:id/hit", function(request, response){
    let pinata = pinataService.getPinataById(request.params.id);
    const data = pinataService.hitPinata(pinata);
    return response.status(data.statusCode).json(data.content);
});
