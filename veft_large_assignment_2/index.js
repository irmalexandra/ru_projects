const express = require('express');
const bodyParser = require('body-parser');
const port = 3000;

const artService = require('./services/artService');
const artistService = require('./services/artistService');
const customerService = require('./services/customerService');
const auctionService = require('./services/auctionService');

const mongoose = require('mongoose');

const server = express();

server.use(bodyParser.json());

server.listen(port, () => {
    console.log(`Server is listening on port: ${port}.`);
});


server.get("/api/arts", async function(request, response){
    let arts = await artService.getAllArts();
    response.status(200);
    return response.json(arts);
});


server.get("/api/arts/:id", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        const art = await artService.getArtById(id);
        if(!(art instanceof Error)){
            if (art) {
                response.status(200);
                return response.json(art);
            } else {
                return response.status(404).json("Art not found.")
            }
        }
        else{
            response.status(500);
            return response.json(art.message)
        }
    }
    else{
        return response.status(404).json("Not a valid art id");
    }
});


server.get("/api/artists", async function(request, response) {
    const artists = await artistService.getAllArtists();
    response.status(200);
    return response.json(artists);
});


server.get("/api/artists/:id", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){

        let artist = await artistService.getArtistById(id);
        if(!(artist instanceof Error)){
            if(artist){
                response.status(200);
                return response.json(artist);
            }
            else{
                response.status(404);
                return response.json("Artist not found.")
            }
        }
        else{
            response.status(404);
            return response.json(artist.message)
        }
    }
    else{
        return response.status(404).json("Not a valid artist id.");
    }
});


server.get('/api/customers', async function(request, response) {
    const customers = await customerService.getAllCustomers();
    response.status(200);
    return response.json(customers);
});


server.get("/api/customers/:id", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        let customer = await customerService.getCustomerById(id);
        if(!(customer instanceof Error)){
            if(customer){
                response.status(200);
                return response.json(customer);
            }
            else{
                response.status(404);
                return response.json("Customer not found.")
            }
        }
        else{
            response.status(404);
            return response.json(customer.message)
        }
    }
    else{
        return response.status(404).json("Not a valid customer id.");
    }
});


server.get("/api/customers/:id/auction-bids", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        await customerService.getCustomerAuctionBids(id, function (returned_bids){
            if(returned_bids){
                response.status(200);
                return response.json(returned_bids);
            }
            else{
                response.status(404);
                return response.json("No bids were found.")
            }
        }, function (error){

            if(error === "Customer not found"){
                response.status(404);
                return response.json("Customer not found.")
            }
            else if(error === "Customer has no bids") {
                response.status(404);
                return response.json("Customer has no bids.")
            }
            else{
                response.status(520);
                return response.json(error.message)
            }

        });
    }
    else{
        return response.status(404).json("Not a valid customer id.");
    }

});


server.get("/api/auctions", async function(request, response){
    let auctions = await auctionService.getAllAuctions();
    response.status(200);
    return response.json(auctions);
});


server.get("/api/auctions/:id", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        let auction = await auctionService.getAuctionById(id);
        response.status(200);
        if(!(auction instanceof Error)){
            if(auction){
                response.status(200);
                return response.json(auction);
            }
            else{
                response.status(404);
                return response.json("Auction not found.");
            }

        }
        else{
            response.status(404);
            return response.json(auction.message);
        }
    }
    else{
        return response.status(404).json("Not a valid auction id.");
    }

});


server.get("/api/auctions/:id/winner", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        let auction_winner = await auctionService.getAuctionWinner(id, function (returned_winner){
            if(!(returned_winner instanceof Error)){
                if(returned_winner){
                    response.status(200);
                    return response.json(returned_winner);
                }
                else if (returned_winner === "This auction has no bids") {
                    return response.status(409).json("This auction has no bids.")
                }
                else {
                    response.status(404);
                    return response.json("No winner found.")
                }
            }
        }, function (error) {
            if(error === "Auction does not exist"){
                response.status(404);
                return response.json("Auction does not exist.")
            }
            else if (error === "The auction is still running") {
                return response.status(409).json("The auction is still running.")
            }
            else {
                response.status(404);
                return response.json(auction_winner.message)
            }
        });}
    else {
        return response.status(404).json("Not a valid auction id.");
    }
});


server.get("/api/auctions/:id/bids", async function(request, response){
    let id = request.params.id;
    if(mongoose.isValidObjectId(id)){
        let auction_bids = await auctionService.getAuctionBidsWithinAuction(id);
        if(!(auction_bids instanceof Error)){
            response.status(200);
            return response.json(auction_bids);
        }
        else{
            response.status(404);
            return response.json(auction_bids.message)
        }
    }
    else{
        return response.status(404).json("Not a valid auction id.");
    }

});

//==================== POST ======================================



server.post("/api/arts", (request, response) => {
    const art = request.body;
    if(mongoose.isValidObjectId(art.artistId)){
        artService.createArt(art, function (returned_art){
                return response.status(200).json(returned_art);
            },
            function (error){
                return response.status(400).json(error)
            });
    } else {
        return response.status(404).json("Not a valid artist id.")
    }

});


server.post("/api/artists", (request, response) => {

    const artist = request.body;
    artistService.createArtist(artist, function (returned_artist){
            return response.status(200).json(returned_artist);
        },
        function (error){
            return response.status(400).json(error)
        });
});


server.post("/api/customers", (request, response) => {
    const costumer = request.body;
    customerService.createCustomer(costumer, function (returned_costumer){
            return response.status(200).json(returned_costumer);
        },
        function (error){
            return response.status(400).json(error)
        });
});


server.post("/api/auctions", (request, response) => {
    const auction = request.body;
    auctionService.createAuction(auction, function (returned_auction){

            return response.status(200).json(returned_auction);
        },
        function (error){
        if(error === "An auction for this art already exists"){
            return response.status(409).json("An auction for this art already exists.")
        }
        else if(error === "Art is not an auction item"){
            return response.status(412).json("Art is not an auction item.")
        }
        else if(error ==="Art not found"){
            return response.status(404).json("Art not found.")
        }
        else{
            return response.status(520).json(error)
        }

        });
});


server.post("/api/auctions/:id/bids", (request, response) => { //auctionId, customerId, price, cb, errorCb
    const body = request.body;
    let id = request.params.id;// IDk man
    let validID = mongoose.isValidObjectId(id);
    let validCustomerId = mongoose.isValidObjectId(body.customerId);
    if(validID && validCustomerId){
        auctionService.placeNewBid(id, body.customerId, body.price, function (returned_art){
                return response.status(200).json(returned_art);
            },
            function (error){
                if(error === "Customer not found"){

                    return response.status(404).json("Customer not found.")
                }
                else if(error === "Auction not found"){

                    return response.status(404).json("Auction not found.")
                }
                else if(error === "Price too low"){
                    return response.status(412).json("Price too low.")
                }
                else if (error === "Auction has completed") {
                    return response.status(403).json("Auction has already completed.")
                }

                return response.status(520).json(error)
            });
    }
    else{
        if(!validID){
            return response.status(404).json("Not a valid auction id.");
        }
        if(!validCustomerId){
            return response.status(404).json("Not a valid customer id.");
        }
    }

});


//==========================================================================

















