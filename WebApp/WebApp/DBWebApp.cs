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
            && w.ClassType.Equals(search.flight.classType)
            && w.Seats >= search.booking.travelers)
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = depName,
                departureTime = s.DepartureTime,
                destination = destName,
                destinationTime = s.DestinationTime,
                classType = s.ClassType,
                price = s.Price
                
            }).OrderBy(o => o.departureTime).ToList();
            if(directRoute.Count != 0)
            {
                for (int i = 0; i < directRoute.Count; i++)
                {
                    searchHit.Add(new List<ViewFlight>());
                    searchHit[i].Add(directRoute[i]);
                }
            }
            else
            {
                List<ViewFlight> tempdep = db.Flight.Where(w => w.Departure == depID
            && w.TravelDate.Equals(search.flight.travelDate)
            && w.ClassType.Equals(search.flight.classType)
            && w.Seats >= search.booking.travelers)
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = depName,
                departureTime = s.DepartureTime,
                destination = db.Airport.Where(a => a.ID == s.Destination).Select(p => p.Name).FirstOrDefault(),
                destinationTime = s.DestinationTime,
                classType = s.ClassType,
                price = s.Price
            }).OrderBy(o => o.departureTime).ToList();

                List<ViewFlight> tempdest = db.Flight.Where(w => w.Destination == destID
            && w.TravelDate.Equals(search.flight.travelDate)
            && w.ClassType.Equals(search.flight.classType)
            && w.Seats >= search.booking.travelers)
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = db.Airport.Where(a => a.ID == s.Departure).Select(p => p.Name).FirstOrDefault(),
                departureTime = s.DepartureTime,
                destination = destName,
                destinationTime = s.DestinationTime,
                classType = s.ClassType,
                price = s.Price
            }).OrderBy(o => o.departureTime).ToList();

                foreach (var x in tempdep)
                {
                    List<ViewFlight> templist = new List<ViewFlight>();
                    foreach (var y in tempdest)
                    {
                        if (x.destination.Equals(y.departure) && TimeSpan.Parse(x.destinationTime).Add(TimeSpan.FromHours(1)) <= TimeSpan.Parse(y.departureTime)
                            && TimeSpan.Parse(x.destinationTime).Add(TimeSpan.FromHours(5)) >= TimeSpan.Parse(y.departureTime))
                        {
                            templist.Add(x);
                            templist.Add(y);
                            break;
                        }
                    }
                    if (templist.Count != 0)
                    {
                        searchHit.Add(templist);
                    }
                }
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
                && w.ClassType.Equals(search.flight.classType)
                && w.Seats >= search.booking.travelers)
                .Select(s => new ViewFlight()
                {
                    id = s.ID,
                    travelDate = s.TravelDate,
                    departure = destName,
                    departureTime = s.DepartureTime,
                    destination = depName,
                    destinationTime = s.DestinationTime,
                    classType = s.ClassType,
                    price = s.Price
                }).OrderBy(o => o.departureTime).ToList();

            if (directReturnRoute.Count != 0)
            {
                for (int i = 0; i < directReturnRoute.Count; i++)
                {
                    searchHit.Add(new List<ViewFlight>());
                    searchHit[i].Add(directReturnRoute[i]);
                }
            }
            else
            {
                List<ViewFlight> tempdep = db.Flight.Where(w => w.Departure == destID
            && w.TravelDate.Equals(search.flight.returnDate)
            && w.ClassType.Equals(search.flight.classType)
            && w.Seats >= search.booking.travelers)
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = destName,
                departureTime = s.DepartureTime,
                destination = db.Airport.Where(a => a.ID == s.Destination).Select(p => p.Name).FirstOrDefault(),
                destinationTime = s.DestinationTime,
                classType = s.ClassType,
                price = s.Price
            }).OrderBy(o => o.departureTime).ToList();

                List<ViewFlight> tempdest = db.Flight.Where(w => w.Destination == depID
            && w.TravelDate.Equals(search.flight.returnDate)
            && w.ClassType.Equals(search.flight.classType)
            && w.Seats >= search.booking.travelers)
            .Select(s => new ViewFlight()
            {
                id = s.ID,
                travelDate = s.TravelDate,
                departure = db.Airport.Where(a => a.ID == s.Departure).Select(p => p.Name).FirstOrDefault(),
                departureTime = s.DepartureTime,
                destination = depName,
                destinationTime = s.DestinationTime,
                classType = s.ClassType,
                price = s.Price
            }).OrderBy(o => o.departureTime).ToList();

                foreach (var x in tempdep)
                {
                    List<ViewFlight> templist = new List<ViewFlight>();
                    foreach (var y in tempdest)
                    {
                        if (x.destination.Equals(y.departure) && TimeSpan.Parse(x.destinationTime).Add(TimeSpan.FromHours(1)) <= TimeSpan.Parse(y.departureTime)
                            && TimeSpan.Parse(x.destinationTime).Add(TimeSpan.FromHours(5)) >= TimeSpan.Parse(y.departureTime))
                        {
                            templist.Add(x);
                            templist.Add(y);
                            break;
                        }
                    }
                    if (templist.Count != 0)
                    {
                        searchHit.Add(templist);
                    }
                }
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
                        travelDate = oneFlight.TravelDate,
                        price = oneFlight.Price
                    };
                    list.Add(returnFlight);
                }
            }
            return list;
        }

        public int getPrice(List<ViewFlight> list)
        {
            int price = 0;
            foreach (var p in list)
            {
                price += p.price;
            }
            return price;
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
                        RoundTrip = final.booking.roundTrip,
                        CardName = final.cardInfo.cardName,
                        CardNr = final.cardInfo.cardNr,
                        Code = final.cardInfo.code,
                        ExpDate = final.cardInfo.expDateMM + "/" + final.cardInfo.expDateYY
                    };
                    contactPerson.Bookings.Add(outBooking);

                }
                updateSeats(final.booking.chosenTravel, db, final.booking.travelers);

                if (final.booking.roundTrip)
                {
                    foreach (var i in final.booking.chosenReturn)
                    {
                        var inBooking = new Booking()
                        {
                            FlightID = i.id,
                            Travelers = final.booking.travelers,
                            RoundTrip = final.booking.roundTrip,
                            CardName = final.cardInfo.cardName,
                            CardNr = final.cardInfo.cardNr,
                            Code = final.cardInfo.code,
                            ExpDate = final.cardInfo.expDateMM + "/" + final.cardInfo.expDateYY
                        };
                        contactPerson.Bookings.Add(inBooking);

                    }
                    updateSeats(final.booking.chosenReturn, db, final.booking.travelers);
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
                            Bookings = contactPerson.Bookings
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

                    }
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception error) { }
            return false;
        }

        private void updateSeats(List<ViewFlight> inList, WebAppContext db, int travelers)
        {
            foreach(var i in inList)
            {
                var update = db.Flight.Find(i.id);
                int seats = update.Seats;
                update.Seats = seats - travelers;
            }
        }
    }
}