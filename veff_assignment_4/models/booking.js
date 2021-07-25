var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var event = require('./event');

var bookingSchema = new Schema({
    firstName: { type: String, required: [true, "Bookings require a firstName!"] },
    lastName: { type: String, required: [true, "Bookings require a lastName!"] },
    tel: { type: String, default: '' },
    email: { type: String, default: '' },
    spots: { type: Number, min: 1.0, required: [true, "Bookings require a spots attribute!"] },
    eventId: { type: Schema.Types.ObjectId, ref: 'events', required: [true, "Events require an eventId!"]}
});

bookingSchema.methods.getPublic = function () {
    var returnObject = {
        firstName: this.firstName,
        lastName: this.lastName,
        tel: this.tel,
        email: this.email,
        spots: this.spots,
        _id: this._id
    };
    return returnObject;
};

module.exports = mongoose.model('bookings', bookingSchema);