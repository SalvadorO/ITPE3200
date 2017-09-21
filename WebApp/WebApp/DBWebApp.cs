using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class DBWebApp
    {
        public bool pushToDataBase(FinalBooking final)
        {
            var newBooking = new Bookings()
            {
                Departure = final.booking.departure,
                Destination = final.booking.destination,
                TravelDate = final.booking.travelDate,
                ReturnDate = final.booking.returnDate,
                ClassType = final.booking.classType,
                Travelers = final.booking.travelers,
                OneWay = final.booking.oneWay
            };

            var contactPerson = new Customers()
            {
                BID = newBooking.BID,
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
                            BID = contactPerson.BID,
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