var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var eventSchema = new Schema({
    name: { type: String, required: [true, "Events require a name!"] },
    description: { type: String, default: '' },
    location: { type: String, default: '' },
    capacity: { type: Number, min: 1.0, required: [true, "Events require positive capacity!"] },
    startDate: { type: Date, required: [true, "Events require a startDate!"] },
    endDate: { type: Date, required: [true, "Events require a endDate!"] }
});

eventSchema.methods.getPublic = function () {
    var returnObject = {
        name: this.name,
        description: this.description,
        location: this.location,
        capacity: this.capacity,
        startDate: this.startDate,
        endDate: this.endDate,
        _id: this._id
    };
    return returnObject;
};

module.exports = mongoose.model('events', eventSchema);