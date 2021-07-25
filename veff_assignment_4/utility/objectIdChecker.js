var mongoose = require('mongoose');

module.exports.isValidObjectID = function(idString) {
    //Only check for existing items
    if (!idString) {
        return true;
    }
    if (!mongoose.Types.ObjectId.isValid(idString)) {
        return false;
    }
    if (!(new mongoose.Types.ObjectId(idString).toString()===idString)) {
        return false;
    }
    return true;
}