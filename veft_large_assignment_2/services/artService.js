const artData = require('../data/db').Art;
const artistData = require('../data/db').Artist;


const artService = () => {

    const globalTryCatch = async cb => {
        try {
            return await cb();
        } catch(err) {
            return err;
        }
    };

    const getAllArts = async () => {
        return await globalTryCatch(async () => {
            return await artData.find({});
        });
    };

    const getArtById = async id => {
        try {
            return await artData.findById(id);
        } catch (error) {
            return error
        }
    };

    const createArt = async (art, callBack, errorCallBack) => {
        let artist = await artistData.findById(art.artistId)
        if (artist) {
            artData.create(art, function(error, result){
                if (error) { errorCallBack(error); }
                else { callBack(result); }
            })
        } else {
            return errorCallBack("Artist does not exist.")
        }
    };

    return {
        getAllArts,
        getArtById,
        createArt
    };
};

module.exports = artService();
