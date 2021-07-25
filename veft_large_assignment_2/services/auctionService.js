const auctionData = require('../data/db').Auction;
const bidData = require('../data/db').AuctionBid;
const artData = require('../data/db').Art;
const getCustomerById = require('./customerService').getCustomerById;



const auctionService = () => {

    const globalTryCatch = async cb => {
        try {
            return await cb();
        } catch(err) {
            return err;
        }
    };

    const getAllAuctions = async () => {
        return await globalTryCatch(async () => {
            return await auctionData.find({});
        });
    };

    const getAuctionById = async id => {
        try {
            return await auctionData.findById(id);
        } catch (error) {
            return error;
        }
    };

    const getAuctionWinner = async (auctionId, callBack, errorCallBack) => {

        const auction = await auctionData.findById(auctionId);

        if(auction){
            if(auction.auctionWinner){
                if (auction.endDate < Date.now()) {
                    let winner = await getCustomerById(auction.auctionWinner);
                    return callBack(winner)
                } else {
                    return errorCallBack("The auction is still running")
                }
            } else {
                return callBack("This auction has no bids");
            }
        }
        else{
            return errorCallBack("Auction does not exist")
        }
    };


    const createAuction = async (auction, callBack, errorCallBack) => {

        let art = await artData.findById(auction.artId);
        if (art) {
            if (art._doc.isAuctionItem){
                let if_auction = await auctionData.findOne({artId : auction.artId}) // Find duplicate

                if (!if_auction){
                    auctionData.create(auction, function(error, result){
                        if (error) { errorCallBack(error); }
                        else { return callBack(result); }
                    })
                } else {
                    return errorCallBack("An auction for this art already exists");
                }
            } else {
                return errorCallBack("Art is not an auction item");
            }
        } else {
            return errorCallBack("Art not found");
        }



    };

	const getAuctionBidsWithinAuction = async auctionId => {
        try {
            return await bidData.find({"auctionId" : auctionId});
        } catch (error) {
            return error;
        }
    };

	const placeNewBid = async function (auctionId, customerId, price, callBack, errorCallBack){
        const customer = await getCustomerById(customerId);
        if(customer){
            const auction = await getAuctionById(auctionId);
            if(auction){
                if (auction.endDate > Date.now()){
                    const auction_bids = await bidData.find({auctionId : auctionId});
                    let highest_bid = 0;
                    for(let i = 0; i < auction_bids.length; i++){
                        if(highest_bid < auction_bids[i].price){
                            highest_bid = auction_bids[i].price
                        }
                    }
                    if(auction._doc.minimumPrice > price || price <= highest_bid){
                        return errorCallBack("Price too low")
                    }

                    bidData.create({
                        auctionId : auctionId,
                        customerId : customerId,
                        price: price
                    },async  function(error, result) {
                        if (error) {
                            errorCallBack(error);
                        } else {
                            auction.auctionWinner = customerId
                            await auction.save()
                            callBack(result);
                        }
                    })
                } else {
                    return errorCallBack ("Auction has completed")
                }
            } else {
                return errorCallBack("Auction not found");
            }
        } else{
            return errorCallBack("Customer not found");
        }

	};

    return {
        getAllAuctions,
        getAuctionById,
        getAuctionWinner,
		createAuction,
		getAuctionBidsWithinAuction,
		placeNewBid
    };
};

module.exports = auctionService();
