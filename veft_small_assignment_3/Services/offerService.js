const data = require("../Data/data.json");


const offerService = () => {
    let allOffers = data.offers;
    let allCandies = data.candies;

    const getAllOffers= () => {
        for (let i = 0; i < allOffers.length; i++){
            if (allOffers[i].candies.length > 0){
                let candyList = allOffers[i].candies;
                for (let j = 0; j < candyList.length; j++){
                    for (let k = 0; k < allCandies.length; k++){
                        if (allCandies[k].id === candyList[j]){
                            allOffers[i].candies[j] = allCandies[k];
                        }
                    }
                }
            }
        }
        return allOffers;
    };

    return {
        getAllOffers
    }
};

module.exports = offerService();