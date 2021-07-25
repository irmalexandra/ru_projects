const customerData = require('../data/db').Customer;
const bidData = require('../data/db').AuctionBid;


const customerService = () => {

    const globalTryCatch = async cb => {
        try {
            return await cb();
        } catch(err) {
            return err;
        }
    };

    const getAllCustomers = async () => {
        return await globalTryCatch(async () => {
            return await customerData.find({});
        });
    };


    const getCustomerById = async id => {
        try {
            return await customerData.findById(id);
        } catch (error) {
            return error;
        }
    };

    const getCustomerAuctionBids = async (customerId, callBack, errorCallBack) => {
        // Your implementation goes here
        const customer = await customerData.findById(customerId);
        if(customer){
            const bids = await bidData.find({customerId : customerId});

            if(bids.length > 0){
                return callBack(bids);
            } else {
                return errorCallBack("Customer has no bids");
            }
        } else {
            return errorCallBack("Customer not found");
        }





    };

    const createCustomer = (customer, callBack, errorCallBack) => {
        // Your implementation goes here
        customerData.create(customer, function(error, result){
            if (error) { errorCallBack(error); }
            else { callBack(result); }
        })
    };

    return {
        getAllCustomers,
        getCustomerById,
        getCustomerAuctionBids,
		createCustomer
    };
};

module.exports = customerService();
