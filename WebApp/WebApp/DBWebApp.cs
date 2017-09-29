using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class DBWebApp
    {
        public List<List<ViewFlight>> searchTravelFlights(ViewModel search)
        {
            var db = new WebAppContext();
            String depName = search.flight.departure;
            String destName = search.flight.destination;
            int depID = db.Airport.Where(a => a.Name.Equals(depName) ).Select(a => a.ID).FirstOrDefault();
            int destID = db.Airport.Where(a => a.Name.Equals(destName)).Select(a => a.ID).FirstOrDefault();
            List<List<ViewFlight>> searchHit = new List<List<ViewFlight>>();

            List<ViewFlight> directRoute = db.Flight.Where(w => w.Departure == depID
            && w.Destination == destID
            && w.TravelDate.Equals(search.flight.travelDate)
            && w.ClassType.Equals(search.flight.classType))
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = depName,
                departureTime = s.DepartureTime,
                destination = destName,
                destinationTime = s.DestinationTime,
                classType = s.ClassType
            }).ToList();

            if(directRoute != null)
            {
                
                for(int i = 0; i < directRoute.Count; i++)
                {
                    searchHit.Add(new List<ViewFlight>());
                    searchHit[i].Add(directRoute.ElementAt(i));
                     searchHit[0].Add(new ViewFlight {
                     id = 1337,
                     travelDate = "test",
                     departure = "test",
                     departureTime = "test",
                     destination = "test",
                     destinationTime = "test",
                     classType = "test"
                 });
                }
            }
            else
            {

            }
            return searchHit;
        }

        public List<List<ViewFlight>> searchReturnFlight(ViewModel search)
        {
            var db = new WebAppContext();
            String depName = search.flight.departure;
            String destName = search.flight.destination;
            int depID = db.Airport.Where(a => a.Name.Equals(depName)).Select(a => a.ID).FirstOrDefault();
            int destID = db.Airport.Where(a => a.Name.Equals(destName)).Select(a => a.ID).FirstOrDefault();
            List<List<ViewFlight>> searchHit = new List<List<ViewFlight>>();

            List<ViewFlight> directReturnRoute = db.Flight.Where(w => w.Departure == destID
                && w.Destination == depID
                && w.TravelDate.Equals(search.flight.returnDate)
                && w.ClassType.Equals(search.flight.classType))
                .Select(s => new ViewFlight()
                {
                    id = s.ID,
                    travelDate = s.TravelDate,
                    departure = depName,
                    departureTime = s.DepartureTime,
                    destination = destName,
                    destinationTime = s.DestinationTime,
                    classType = s.ClassType
                }).ToList();

            if (directReturnRoute != null)
            {
                for (int i = 0; i < directReturnRoute.Count; i++)
                {
                    searchHit.Add(new List<ViewFlight>());
                    searchHit[i].Add(directReturnRoute[i]);
                    searchHit[0].Add(new ViewFlight
                    {
                        id = 1337,
                        travelDate = "test",
                        departure = "test",
                        departureTime = "test",
                        destination = "test",
                        destinationTime = "test",
                        classType = "test"
                    });
                }
            }
            else
            {

            }
            return searchHit;
        }

        public List<List<int>> filterIDs(List<List<ViewFlight>> inList)
        {
            List<List<int>> returnList = new List<List<int>>();
            for(int x = 0; x < inList.Count; x++)
            {
                returnList.Add(new List<int>());
                for( int y = 0; y < inList[x].Count; y++ )
                {
                    returnList[x].Add(inList[x][y].id);
                }
            }
            return returnList;
        }

        public ViewFlight getFlight(int id)
        {
            var db = new WebAppContext();
            var oneFlight = db.Flight.Find(id);

            if(oneFlight == null)
            {
                return null;
            }
            else
            {
                String depName = db.Airport.Where(a => a.ID == oneFlight.Departure).Select(a => a.Name).FirstOrDefault();
                String destName = db.Airport.Where(a => a.ID == oneFlight.Destination).Select(a => a.Name).FirstOrDefault();
                var returnFlight = new ViewFlight()
                {
                    id = oneFlight.ID,
                    departure = depName,
                    departureTime = oneFlight.DepartureTime,
                    destination = destName,
                    destinationTime = oneFlight.DestinationTime,
                    classType = oneFlight.ClassType
                };
                return returnFlight;
            }

        }
        public bool pushToDataBase(ViewModel final)
        {
            var newBooking = new Booking()
            {
                Travelers = final.booking.travelers,
                RoundTrip = final.booking.roundTrip,
                TravelFlightID = final.booking.flightId
            };

            var contactPerson = new Customer()
            {
               // BookingsID = newBooking.ID,
                FirstName = final.customers[0].firstName,
                LastName = final.customers[0].lastName,
                PhoneNumber = final.customers[0].phoneNumber,
                EMail = final.customers[0].eMail,
                ContactPerson = true
            };

            var db = new WebAppContext();
            try
            {
                db.Booking.Add(newBooking);
                db.Customer.Add(contactPerson);

                if (final.booking.travelers > 1)
                {
                    for (int i = 1; i < final.booking.travelers; i++)
                    {
                        var customer = new Customer()
                        {
                           // BookingsID = contactPerson.BookingsID,
                            FirstName = final.customers[i].firstName,
                            LastName = final.customers[i].lastName,
                            PhoneNumber = final.customers[i].phoneNumber,
                            EMail = final.customers[i].eMail,
                            ContactPerson = false
                        };
                        db.Customer.Add(customer);
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