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
                }
                searchHit[0].Add(new ViewFlight
                {
                    id = 8,
                    travelDate = "30/09/2017",
                    departure = "Tromsø",
                    departureTime = "14:00",
                    destination = "Hammerfest",
                    destinationTime = "15:00",
                    classType = "Økonomi"
                });
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

        public List<ViewFlight> getFlights(List<int> inList)
        {
            var db = new WebAppContext();
            List<ViewFlight> list = new List<ViewFlight>();

            foreach (var item in inList) {
                var oneFlight = db.Flight.Find(item);

                if (oneFlight == null)
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
                        classType = oneFlight.ClassType,
                        travelDate = oneFlight.TravelDate
                    };
                    list.Add(returnFlight);
                }
            }
            return list;
        }

        public bool pushToDataBase(ViewModel final)
        {
            var db = new WebAppContext();
            try
            {

                var contactPerson = new Customer()
                {
                    FirstName = final.customers[0].firstName,
                    LastName = final.customers[0].lastName,
                    PhoneNumber = final.customers[0].phoneNumber,
                    EMail = final.customers[0].eMail,
                    Address = final.customers[0].address,
                    ZipCode = final.customers[0].zipCode,
                    ContactPerson = true,
                    Bookings = new List<Booking>()
            };


                var existingZip = db.City.Find(final.customers[0].zipCode);

                if (existingZip == null)
                {
                    var newCity = new City()
                    {
                        ZipCode = final.customers[0].zipCode,
                        CityName = final.customers[0].city
                    };
                    contactPerson.Cities = newCity;
                }
                db.Customer.Add(contactPerson);


                foreach (var i in final.booking.chosenTravel)
                {
                    var outBooking = new Booking()
                    {
                        FlightID = i.id,
                        Travelers = final.booking.travelers,
                        RoundTrip = final.booking.roundTrip
                    };
                    contactPerson.Bookings.Add(outBooking);

                }
                if (final.booking.roundTrip)
                {
                    foreach (var i in final.booking.chosenReturn)
                    {
                        var inBooking = new Booking()
                        {
                            FlightID = i.id,
                            Travelers = final.booking.travelers,
                            RoundTrip = final.booking.roundTrip
                        };
                        contactPerson.Bookings.Add(inBooking);

                    }
                }




                if (final.booking.travelers > 1)
                {
                    for (int i = 1; i < final.booking.travelers; i++)
                    {
                        var customer = new Customer()
                        {
                            FirstName = final.customers[i].firstName,
                            LastName = final.customers[i].lastName,
                            PhoneNumber = final.customers[i].phoneNumber,
                            EMail = final.customers[i].eMail,
                            Address = final.customers[i].address,
                            ZipCode = final.customers[i].zipCode,
                            ContactPerson = false,
                            Bookings = new List<Booking>()
                        };

                        var eZip = db.City.Find(final.customers[i].zipCode);

                        if (eZip == null)
                        {
                            var newCity = new City()
                            {
                                ZipCode = final.customers[i].zipCode,
                                CityName = final.customers[i].city
                            };
                            customer.Cities = newCity;
                        }

                        db.Customer.Add(customer);

                        foreach (var x in final.booking.chosenTravel)
                        {
                            var outBooking = new Booking()
                            {
                                FlightID = x.id,
                                Travelers = final.booking.travelers,
                                RoundTrip = final.booking.roundTrip
                            };

                            customer.Bookings.Add(outBooking);

                        }
                        if (final.booking.roundTrip)
                        {
                            foreach (var x in final.booking.chosenReturn)
                            {
                                var inBooking = new Booking()
                                {
                                    FlightID = x.id,
                                    Travelers = final.booking.travelers,
                                    RoundTrip = final.booking.roundTrip
                                };

                                customer.Bookings.Add(inBooking);

                            }
                        }

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