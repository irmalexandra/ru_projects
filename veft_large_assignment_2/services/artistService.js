const artistData = require('../data/db').Artist;

const artistService = () => {

    const globalTryCatch = async cb => {
        try {
            return await cb();
        } catch(err) {
            return err;
        }
    };


    const getAllArtists = async () => {
        return await globalTryCatch(async () => {
            return await artistData.find({});
        });
    };
    const getArtistById = async id => {
        try {
            return await artistData.findById(id);
        } catch (error) {
            return error;
        }
    };


    const createArtist = (artist, callBack, errorCallBack) => {
        // Your implementation goes here
        artistData.create(artist, function(error, result){
            if (error) {
                errorCallBack(error);
            } else {
                callBack(result);
            }
        })
    };
    return {
        getAllArtists,
        getArtistById,
        createArtist
    };
};

module.exports = artistService();
