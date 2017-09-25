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
            /*List<Flight> searchHit = db.Flights
                .Where(s => s.Departure == search.flight.departure && s.Destination == search.flight.departure)
                .Select(h => new Flight()
                {
                   id = h.ID,
                     departureTime = h.DepartureTime,
                     departure = h.Departure,
                     destinationTime = h.DestinationTime,
                     destination = h.Destination,
                     travelDate = h.TravelDate,
                     returnDate = h.ReturnDate,
                     classType = h.ClassType
                }).ToList();*/
            List<Flight> searchHit = new List<Flight>()
            {
                new Flight
                {
                    departureTime = "TIDUT",
                    departure = "UT",
                    destinationTime = "TIDLAND",
                    destination = "LAND",
                    travelDate = "DATOUT",
                    returnDate = "DATOHJEM",
                    classType = "Luxus"
                }
            };
            return searchHit;
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