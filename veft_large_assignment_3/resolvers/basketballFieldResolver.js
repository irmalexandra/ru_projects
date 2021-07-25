const basketBallFieldService = require('../services/basketballFieldService');

module.exports = {
    queries: {
        allBasketballFields: basketBallFieldService.allBasketballFields,
        basketballField: basketBallFieldService.getBasketballFieldById
    },
    types: {

    },
    mutations: {

    },
    getFieldById : basketBallFieldService.getBasketballFieldById,
};


