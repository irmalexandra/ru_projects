const axios = require('axios');
const basketBallFieldUri = "https://basketball-fields.herokuapp.com/api/basketball-fields";


async function allBasketballFields(parent, args){

    const receivedData = await axios.get(basketBallFieldUri);
    const fields = receivedData["data"];
    if (args["status"]){
        statusFields = [];
        for (field in fields){
            if (fields[field].status === args["status"]){
                statusFields.push(fields[field])
            }
        }
        return statusFields
    }
    else{
        return fields
    }

}

async function getBasketballFieldById(parent, args){
    let field;
    if (args.id){
        field = await axios.get(basketBallFieldUri+"/"+args.id);
    }
    else{
        field = await axios.get(basketBallFieldUri+"/"+args);
    }
    return field["data"]
}

module.exports = {
    allBasketballFields: allBasketballFields,
    getBasketballFieldById: getBasketballFieldById
};