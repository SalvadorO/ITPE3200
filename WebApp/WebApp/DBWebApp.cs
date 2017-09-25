using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class DBWebApp
    {
        public List<Flight> searchFlights(SearchBooking search)
        {
            var db = new WebAppContext();

            String depName = search.flight.departure;
            String destName = search.flight.destination;
            int depID = db.Airports.Where(a => a.Name.Equals(depName) ).Select(a => a.ID).FirstOrDefault();
            int destID = db.Airports.Where(a => a.Name.Equals(destName)).Select(a => a.ID).FirstOrDefault();

            List<Flight> searchHit = db.Flights
                .Where(w => w.Departure == depID && w.Destination == destID)
                .Select(s => new Flight()
                {
                    id = s.ID,
                    departure = depName,
                    departureTime = s.DepartureTime,
                    destination = destName,
                    destinationTime = s.DestinationTime,
                    travelDate = s.TravelDate,
                    classType = s.ClassType
                }).ToList();
                
            return searchHit;
        }

        public Flight getFlight(int id)
        {
            var db = new WebAppContext();
            var oneFlight = db.Flights.Find(id);

            if(oneFlight == null)
            {
                return null;
            }
            else
            {
                String depName = db.Airports.Where(a => a.ID == oneFlight.Departure).Select(a => a.Name).FirstOrDefault();
                String destName = db.Airports.Where(a => a.ID == oneFlight.Destination).Select(a => a.Name).FirstOrDefault();
                var returnFlight = new Flight()
                {
                    id = oneFlight.ID,
                    departure = depName,
                    departureTime = oneFlight.DepartureTime,
                    destination = destName,
                    destinationTime = oneFlight.DestinationTime,
                    travelDate = oneFlight.TravelDate,
                    classType = oneFlight.ClassType
                };
                return returnFlight;
            }

        }
        public bool pushToDataBase(FinalBooking final)
        {
            var newBooking = new Bookings()
            {
                Travelers = final.booking.travelers,
                OneWay = final.booking.oneWay,
                FlightID = final.booking.flightId
            };

            var contactPerson = new Customers()
            {
                BookingID = newBooking.ID,
                FirstName = final.customers[0].firstName,
                LastName = final.customers[0].lastName,
                PhoneNumber = final.customers[0].phoneNumber,
                EMail = final.customers[0].eMail,
                ContactPerson = true
            };

            var db = new WebAppContext();
            try
            {
                db.Bookings.Add(newBooking);
                db.Customers.Add(contactPerson);

                if (final.booking.travelers > 1)
                {
                    for (int i = 1; i < final.booking.travelers; i++)
                    {
                        var customer = new Customers()
                        {
                            BookingID = contactPerson.BookingID,
                            FirstName = final.customers[i].firstName,
                            LastName = final.customers[i].lastName,
                            PhoneNumber = final.customers[i].phoneNumber,
                            EMail = final.customers[i].eMail,
                            ContactPerson = false
                        };
                        db.Customers.Add(customer);
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception error) { }
            return false;
        }
    }
}