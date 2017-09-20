using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class DBWebApp
    {
        public Booking flightRegistration(Booking inFlightBooking)
        {
                var newBooking = new Booking();
                newBooking.adult = inFlightBooking.adult;
                newBooking.child = inFlightBooking.child;
                newBooking.classType = inFlightBooking.classType;
                newBooking.departure = inFlightBooking.departure;
                newBooking.destination = inFlightBooking.destination;
                newBooking.oneWay = inFlightBooking.oneWay;
                newBooking.travelDate = inFlightBooking.travelDate;
                newBooking.returnDate = inFlightBooking.returnDate;
                return newBooking;
        }
    }
}