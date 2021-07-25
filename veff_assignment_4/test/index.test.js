//Importing the application to test
let server = require('../index');
let mongoose = require("mongoose");
let Event = require('../models/event');
let Booking = require('../models/booking');

//These are the actual modules we use
let chai = require('chai');
let should = chai.should();
let chaiHttp = require('chai-http');
chai.use(chaiHttp);

describe('Endpoint tests', () => {
    //###########################
    //These variables contain the ids of the existing event/booking
    //That way, you can use them in your tests (e.g., to get all bookings for an event)
    //###########################
    let eventId = '';
    let bookingId = '';

    //###########################
    //The beforeEach function makes sure that before each test, 
    //there is exactly one event and one booking (for the existing event).
    //The ids of both are stored in eventId and bookingId
    //###########################
    beforeEach((done) => {
        let event = new Event({
             name: "Test Event",
              capacity: 10,
              startDate: 1590840000000,
              endDate: 1590854400000

            });

        Event.deleteMany({}, (err) => {
            Booking.deleteMany({}, (err) => {
                event.save((err, ev) => {
                    let booking = new Booking({ eventId: ev._id, firstName: "Jane", lastName: "Doe", email: "jane@doe.com", spots: 2 });
                    booking.save((err, book) => {
                        eventId = ev._id;
                        bookingId = book._id;
                        done();
                    });
                });
            });
        });
    });

    //###########################
    //Write your tests below here
    //###########################

    it("should always pass", function() {
        console.log("Our event has id " + eventId);
        console.log("Our booking has id " + bookingId);
        chai.expect(1).to.equal(1);
    });

    describe("GET & POST endpoint tests", () => {
        describe("endpoint #1 test", ()=> {
            it('Get All Events', (done) => {
                chai.request('http://localhost:3000/api/v1/events').get('/').end((err, res) => {
                    chai.expect(res).to.have.status(200);
                    chai.expect(res).to.be.json;
                    chai.expect(res.body).to.be.an('array');
                    chai.expect(res.body.length).to.eql(1);
                    done()
                })
            })
        });
        describe("endpoint #2 test", ()=> {
            it('Get Specific Event', (done) => {
                chai.request('http://localhost:3000/api/v1').get('/events/' + eventId).end((err, res) => {
                    chai.expect(res).to.have.status(200);
                    chai.expect(res).to.be.json;
                    chai.expect(Object.keys(res.body).length).to.eql(8);
                    chai.expect(res.body).to.be.a('object');
                    chai.expect(res.body).to.have.property('description').eql('');
                    chai.expect(res.body).to.have.property('location').eql('');
                    chai.expect(res.body).to.have.property('_id').eql(eventId.toString());
                    chai.expect(res.body).to.have.property('name').eql('Test Event');
                    chai.expect(res.body).to.have.property('capacity').eql(10);
                    chai.expect(res.body).to.have.property('startDate');
                    chai.expect(res.body).to.have.property('endDate');
                    chai.expect(res.body).to.have.property('bookings');
                    chai.expect(res.body.bookings[0]).eql(bookingId.toString());
                    done()
                })
            })
        });
        describe("endpoint #3 test", ()=> {
            it('Post a Event', function(done) {
                chai.request('http://localhost:3000/api/v1').post('/events/').type('JSON').send({
                    "name": "MartianMars",
                    "capacity": 100000,
                    "startDate": "2020-03-14T02:02:02.000Z",
                    "endDate": "2020-03-25T08:05:03.000Z",
                    "_id": "5e78b06eccacf926ec9b06a2"
                }).end((err, res) => {
                    chai.expect(res).to.have.status(201);
                    chai.expect(res).to.be.json;
                    chai.expect(Object.keys(res.body).length).to.eql(7);
                    chai.expect(res.body).to.be.a('object');
                    chai.expect(res.body).to.have.property('name').eql('MartianMars');
                    chai.expect(res.body).to.have.property('capacity').eql(100000);
                    chai.expect(res.body).to.have.property('_id');
                    chai.expect(res.body).to.have.property('startDate');
                    chai.expect(res.body).to.have.property('endDate');
                    done();
                })
            })
        });
        describe("endpoint #4 test", ()=> {
            it('Get bookings for a specific event', (done) => {
                chai.request('http://localhost:3000/api/v1').get('/events/' + eventId + '/bookings').end((err, res) => {
                    chai.expect(res).to.have.status(200);
                    chai.expect(res).to.be.json;
                    chai.expect(res.body).to.be.an('array');
                    chai.expect(res.body.length).to.eql(1);
                    done()
                })
            })
        });
        describe("endpoint #5 test", ()=> {
            it('Get specific booking for a specific Event', (done) => {
                chai.request('http://localhost:3000/api/v1').get('/events/' + eventId + '/bookings/' + bookingId).end((err, res) => {
                    chai.expect(res).to.have.status(200);
                    chai.expect(res).to.be.json;
                    chai.expect(Object.keys(res.body).length).to.eql(6);
                    chai.expect(res.body).to.be.a('object');
                    chai.expect(res.body).to.have.property('tel').eql('');
                    chai.expect(res.body).to.have.property('email').eql('jane@doe.com');
                    chai.expect(res.body).to.have.property('_id').eql(bookingId.toString());
                    chai.expect(res.body).to.have.property('firstName').eql('Jane');
                    chai.expect(res.body).to.have.property('lastName').eql('Doe');
                    chai.expect(res.body).to.have.property('spots').eql(2);
                    done()
                })
            })
        });
        describe("endpoint #6 test", ()=> {
            it('Post a booking for a specific Event', function(done){
                chai.request('http://localhost:3000/api/v1').post('/events/'+ eventId + "/bookings").type('JSON').send({
                    "firstName": "Emmi",
                    "lastName": "Kjellinn",
                    "tel": 4658734,
                    "email": "IHateThisSyntax@wtf.com",
                    "spots": 2
                }).end((err, res) => {
                    chai.expect(res).to.have.status(201);
                    chai.expect(res).to.be.json;
                    chai.expect(Object.keys(res.body).length).to.eql(6);
                    chai.expect(res.body).to.be.a('object');
                    chai.expect(res.body).to.have.property('firstName').eql('Emmi');
                    chai.expect(res.body).to.have.property('lastName').eql('Kjellinn');
                    chai.expect(res.body).to.have.property('_id');
                    chai.expect(res.body).to.have.property('tel').eql('4658734');
                    chai.expect(res.body).to.have.property('email').eql('IHateThisSyntax@wtf.com');
                    chai.expect(res.body).to.have.property('spots').eql(2);
                    done();
                })
            })
        })
    });


    describe("Delete tests", ()=> {
        describe("endpoint #8 test", () => {
            it('Delete a specific Booking for a specific Event with correct credentials', (done) => {
                chai.request('http://localhost:3000/api/v1').delete('/events/' + eventId + "/bookings/" + bookingId).auth("admin", "secret").end((err, res) => {
                    chai.expect(res).to.have.status(200);
                    done();
                })
            })
        
            it('Delete a specific Booking for a specific Event with incorrect credentials', (done) => {
                chai.request('http://localhost:3000/api/v1').delete('/events/' + eventId + "/bookings/" + bookingId).auth("notadmin", "notsecret").end((err, res) => {
                    chai.expect(res).to.have.status(401);
                    done();
                 })
            })
        })
    });
});

