const getCandyModel = require("../Models/candy").getCandyModel;
const data = require("../Data/data.json");


const candyService = () => {
    let allCandies = data.candies;

    const getAllCandies = () => {
        return allCandies;
    };

    const getCandyById = id => {
        for (let i = 0; i < allCandies.length; i++){
            if (allCandies[i].id === parseInt(id)){
                return allCandies[i];
            }
        }
        return null;
    };

    const createCandy = candy => {
        let newCandy = getCandyModel();

        newCandy.name = candy.name;
        newCandy.description = candy.description;
        newCandy.id = allCandies.length + 1;

        allCandies.push(newCandy);
        return newCandy;
    };

    return {
        getAllCandies,
        getCandyById,
        createCandy
    }


};

module.exports = candyService();