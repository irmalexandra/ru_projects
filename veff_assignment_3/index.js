// database //
// -------------------------------------------------------------------------------------//

var events = [
    { id: 0, name: "The Whistlers", description: "Romania, 2019, 97 minutes", location: "Bio Paradís, Salur 1", capacity: 40, startDate: new Date(Date.UTC(2020, 02, 03, 22, 0)), endDate: new Date(Date.UTC(2020, 02, 03, 23, 45)), bookings: [0,1,2] },
    { id: 1, name: "HarpFusion: Bach to the Future", description: "Harp ensemble", location: "Harpa, Hörpuhorn", capacity: 100, startDate: new Date(Date.UTC(2020, 02, 12, 15, 0)), endDate: new Date(Date.UTC(2020, 02, 12, 16, 0)), bookings: [] }
];

var bookings = [
    { id: 0, firstName: "John", lastName: "Doe", tel: "+3541234567", email: "", spots: 3},
    { id: 1, firstName: "Jane", lastName: "Doe", tel: "", email: "jane@doe.doe", spots: 1},
    { id: 2, firstName: "Meðaljón", lastName: "Jónsson", tel: "+3541111111", email: "mj@test.is", spots: 5}
];

// CONSTANTS //
const express = require('express');
const cors = require('cors')
const bodyParser = require('body-parser');

const server = express();
server.use(bodyParser.json());
server.use(cors());

const port = process.env.PORT || 3000;

var eventsLength = events.length;
var bookingsLength = bookings.length;


// ---------------------------------------------------------------//

// functions //
// ---------------------------------------------------------------------//

function makeEventList(){
    var eventArr = [];
    for (var i = 0; i < events.length; i++){
        var newObj = {
            id:events[i].id,
            name:events[i].name,
            capacity:events[i].capacity,
            startDate:events[i].startDate,
            endDate:events[i].endDate
        };
        eventArr.push(newObj)
    }
    return eventArr
}

function findEvent(eventID){
    for (var i = 0; i < events.length; i++){
        if (eventID === events[i].id) {
            return events[i]
        }
    }
    return -1
}

function findBooking(bookingID){
    for(var i = 0; i < bookings.length; i++){
        if(bookingID === bookings[i].id){
            return bookings[i]
        }
    }
}

function findBookingForEvent(bookingsIDArr, id){

    if (bookingsIDArr.includes(id)){
        for (var i = 0; i < bookings.length; i++){
            if (id === bookings[i].id) {
                return bookings[i]
            }
        }
    }
    return -1
}

function makeBookingList(bookingsIDArr){
    var eventBookingsArr = [];
    for (var i = 0; i < bookings.length; i++){
        for(var j = 0; j < bookingsIDArr.length; j++){
            if (bookings[i].id === bookingsIDArr[j]) {
                eventBookingsArr.push(bookings[i])
            }
        }

    }
    return eventBookingsArr
}

function createEvent(eventDetails){
    if (validateEventInfo(eventDetails)){
        var dateArr = eventDetails.startDate.split(" ");
        var sdate = dateArr[0]
        var stime = dateArr[1]
        var startDate = sdate.split("-");
        var startTime = stime.split(":");

        var dateArr = eventDetails.endDate.split(" ");
        var edate = dateArr[0]
        var etime = dateArr[1]
        var endDate = edate.split("-");
        var endTime = etime.split(":");

        if(eventDetails.description == undefined){eventDetails.description = ""}
        if(eventDetails.location == undefined){eventDetails.location = ""}

        let event = {
            id: generateEventID(),
            name: eventDetails.name,
            description: eventDetails.description,
            location: eventDetails.location,
            capacity: parseInt(eventDetails.capacity),
            startDate: new Date(Date.UTC(
                parseInt(startDate[0]), // Year
                parseInt(startDate[1]), // Month
                parseInt(startDate[2]), // Date
                parseInt(startTime[0]), // Hours
                parseInt(startTime[1])  // Minutes
            )),
            endDate: new Date(Date.UTC(
                parseInt(endDate[0]), // Year
                parseInt(endDate[1]), // Month
                parseInt(endDate[2]), // Date
                parseInt(endTime[0]), // Hours
                parseInt(endTime[1])  // Minutes
            )),
            bookings: []
        };
        events.push(event);
        return event
    }
    else{
        return -1
    }
}

function createBooking(bookingDetails, currentEvent){
    if(validateBookingInfo(bookingDetails)){
        if(bookingDetails.tel == undefined){bookingDetails.tel = ""}
        if(bookingDetails.email == undefined){bookingDetails.email = ""}
        let booking = {
            id: generateBookingID(),
            firstName: bookingDetails.firstName,
            lastName: bookingDetails.lastName,
            tel: bookingDetails.tel,
            email: bookingDetails.email,
            spots: bookingDetails.spots
        };
        currentEvent.bookings.push(booking.id)
        bookings.push(booking)
        return booking
    }
    else {
        return -1
    }
}

function validateID(id) {
    if (isNaN(id)) {
        return false
    }
    return true
}

function validateEventInfo(eventDetails) {
    let capOverOrEqualZero = false;
    const validStartDate = (new Date(eventDetails.startDate)).getTime() > 0;

    const validEndDate = (new Date(eventDetails.endDate)).getTime() > 0;

    const validCap = Number.isInteger(parseInt(eventDetails.capacity));
    if(validCap){capOverOrEqualZero = parseInt(eventDetails.capacity) >= 0;}

    if(validStartDate && validEndDate && validCap && capOverOrEqualZero){return true}
    return false
}

function validateBookingInfo(bookingDetails) {
    if(bookingDetails.tel != undefined || bookingDetails.email != undefined){
        if (Number.isInteger(parseInt(bookingDetails.spots))) {
            let spotsOverZero = bookingDetails.spots > 0;
            let validSpots = false;
            if(spotsOverZero){validSpots = checkSpots(bookingDetails.spots, bookingDetails.eventID)}
            if(spotsOverZero && validSpots){return true}
        }
    }
    return false

}

function checkSpots(spots, eventID) {
    
    let currentEvent = findEvent(eventID);
    let currentBookingsIDs = currentEvent.bookings;
    let currentBookings = makeBookingList(currentBookingsIDs);
    let availableSpots = currentEvent.capacity
    for(var i = 0; i < currentBookings.length; i++){
        availableSpots -= currentBookings[i].spots
    }
    availableSpots -= spots
    if(availableSpots > 0){return true}

    return false
}

function generateEventID() {
    var eventID = eventsLength
    eventsLength++
    return eventID
}

function generateBookingID() {
    var bookingID = bookingsLength
    bookingsLength++
    return bookingID
}

function findArrayIndex(objectID, arr){
    for(var i = 0; i < arr.length; i++){
        if(objectID === arr[i].id){return i}
    }
    return -1
}

function deleteAllEvents() {
    if(events.length > 0){
        let retEvents = [];
        for(var i = 0; i < events.length; i++){
            let bookingsArr = makeBookingList(events[i].bookings)
            events[i].bookings = bookingsArr;
        }
        retEvents = events;
        events = []
        bookings = []
        return retEvents;
    }
    return -1
}

function deleteEvent(retEvent) {
    
    if(retEvent.bookings.length === 0) {
        let eventIndex = findArrayIndex(retEvent.id, events);
        if (eventIndex !== -1) {
            events.splice(eventIndex, 1);
            return 1
        }
    }
    return -1
}

function deleteBooking(bookingID, retEvent) {
    let eventBookings = retEvent.bookings
    if (eventBookings.includes(bookingID)) {
        for (var i = 0; i < eventBookings.length; i++){
            if (bookingID === eventBookings[i]) {
                eventBookings.splice(i, 1)
            }   
        }
        let retBooking = findBooking(bookingID)
        if(retBooking !== -1){
            let bookingIndex = findArrayIndex(bookingID, bookings)
            if(bookingIndex !== -1){
                bookings.splice(bookingIndex, 1)
                return retBooking
            }
        }
    }
    return -1
}

function deleteAllBookings(currentEvent) {
    if(bookings.length > 0){
        
        let retBookingsIDs = currentEvent.bookings;
        currentEvent.bookings = [];
        let retBookings = makeBookingList(retBookingsIDs)
        return retBookings;
    }
    return -1
}

function updateEvent(updatedEvent) {
    let eventID = updatedEvent.eventID
    if(eventID !== undefined){
        let currentEvent = findEvent(eventID)
        if (currentEvent !== -1) {
            if(currentEvent.bookings.length === 0){
                if(validateEventInfo(updatedEvent)){
                    currentEvent.name = updatedEvent.name
                    currentEvent.capacity = updatedEvent.capacity
                    currentEvent.startDate = updatedEvent.startDate
                    currentEvent.endDate = updatedEvent.endDate
                    currentEvent.location = updatedEvent.location
                    currentEvent.description = updatedEvent.description
                    return currentEvent
                }
            }
        }

    }
    return -1
}


// Endpoints //
// ---------------------------------------------------------------------//

// Server listener
server.listen(port, () => {
    console.log("listening on port " + port)
});

// Read All Events
server.get("/api/v1/events", (req, res) => {
    eventList = makeEventList();
    
    if (eventList.length > 0) {
        res.status(200).send(eventList);
    }
    else {
        res.status(404).send("No events found")
    }
});

// Read Individual Event
server.get("/api/v1/events/event/:eventID", (req, res) => {

    if (validateID(req.params.eventID)) {
        var eventID = parseInt(req.params.eventID);
        var eventObj = findEvent(eventID);
        if (eventObj != -1){
            res.status(200).send(eventObj);
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }

});

// Read all Bookings
server.get("/api/v1/events/event/:eventID/bookings", (req, res) => {
    if (validateID(req.params.eventID)) {
        var eventID = parseInt(req.params.eventID);
        var eventObj = findEvent(eventID);
        if (eventObj != -1){
            var bookingsArr = eventObj.bookings;
            if (bookingsArr.length > 0){
                var eventBookingsArr = makeBookingList(bookingsArr);
                res.status(200).send(eventBookingsArr);
            }
            else {
                res.status(404).send("No bookings found for this event.")
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        } 
    }
    else {
        res.status(400).send("Invalid ID format")
    }

});

// Read Individual Booking
server.get("/api/v1/events/event/:eventID/bookings/booking/:bookingID", (req, res) => {
    if (validateID(req.params.eventID) && validateID(req.params.bookingID)) {
        let eventID = parseInt(req.params.eventID)
        if (findEvent(eventID) !== -1) {
            var bookingID = parseInt(req.params.bookingID);
            var eventObj = findEvent(eventID);
            var bookingsIDArr = eventObj.bookings;
            var booking = findBookingForEvent(bookingsIDArr, bookingID);
            if (booking !== -1){
                res.status(200).send(booking);
            }
            else {
                res.status(404).send("Booking not found for this event.")
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }
});

// Create Event
server.post("/api/v1/events", (req, res) => {
    var eventDetails = req.body;
    var retEvent = createEvent(eventDetails);
    if(retEvent !== -1){
        res.status(201).send(retEvent)
    }
    else{
        res.status(400).send("Invalid event info.")
    }
});

// Create Booking
server.post("/api/v1/events/event/bookings", (req, res) => {
    var bookingDetails = req.body;
    if (validateID(bookingDetails.eventID)) {
        var eventID = parseInt(bookingDetails.eventID)
        let currentEvent = findEvent(eventID);
        if (currentEvent !== -1) {
            
            var retBooking = createBooking(bookingDetails, currentEvent);

            if(retBooking !== -1){
                res.status(201).send(retBooking)
            }
            else{
                res.status(400).send("Invalid booking info.")
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }
});

// Delete Event
server.delete("/api/v1/events/event/:eventID", (req , res) => {
    if (validateID(req.params.eventID)) {
        let eventID = parseInt(req.params.eventID)
        let retEvent = findEvent(eventID);
        if (retEvent !== -1) {
            ifSuccess = deleteEvent(retEvent)

            if (ifSuccess !== -1) {
                res.status(200).send(retEvent)
            }
            else {
                res.status(400).send("There are bookings for this event.")
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }
});

// Delete Booking
server.delete("/api/v1/events/event/:eventID/bookings/booking/:bookingID", (req, res) => {
    if (validateID(req.params.eventID) && validateID(req.params.eventID)) {
        let eventID = parseInt(req.params.eventID)
        let retEvent = findEvent(eventID)
        if(retEvent !== -1){
            
            let bookingID = parseInt(req.params.bookingID)
            let retBooking = deleteBooking(bookingID, retEvent)
            if(retBooking !== -1){
                res.status(200).send(retBooking)
            }
            else{
                res.status(400).send("Invalid booking info")
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }
});

// Delete All Events
server.delete("/api/v1/events", (req, res) =>{
    let retEvents = deleteAllEvents()
    if(retEvents !== -1){
        res.status(200).send(retEvents)
    }
    else{
        res.status(400).send("Error when deleting all events")
    }
});

// Delete all bookings for event
server.delete("/api/v1/events/event/:eventID/bookings", (req, res) => {
    if (validateID(req.params.eventID)) {
        let eventID = parseInt(req.params.eventID)
        let retEvent = findEvent(eventID);
        if (retEvent !== -1) {

            let retBookings = deleteAllBookings(retEvent)
            if(retBookings !== -1) {
                res.status(200).send(retBookings)
            }
            else {
                res.status(400).send("Error when deleting all bookings for event "+ eventID)
            }
        }
        else {
            res.status(404).send("No event with id " + eventID + " found.")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }
});

// Update
server.put("/api/v1/events/event", (req, res) => {
    const eventID = req.body.eventID
    if (validateID(eventID)){
        let updatedEvent = updateEvent(req.body)

        if(updatedEvent !== -1) {
            res.status(200).send(updatedEvent)
        }
        else {
            res.status(400).send("Invalid update info")
        }
    }
    else {
        res.status(400).send("Invalid ID format")
    }


});

// Default for not allowed statements
server.use("*", (req, res) => {
    res.status(405).send("This request is not allowed.")
});



