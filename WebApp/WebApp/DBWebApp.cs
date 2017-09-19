using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class DBWebApp
    {
        public bool flight(Booking inFlight)
        {
            try
            {
                var newBooking = new Booking();
                newBooking.adult = inFlight.adult;
                newBooking.child = inFlight.child;
                newBooking.classType = inFlight.classType;
                newBooking.departure = inFlight.departure;
                newBooking.destination = inFlight.destination;
                newBooking.oneWay = inFlight.oneWay;
                newBooking.travelDate = inFlight.travelDate;
                newBooking.returnDate = inFlight.returnDate;
                return true;
            }
            catch (Exception error) {
                return false;
            }
        }
    }
}