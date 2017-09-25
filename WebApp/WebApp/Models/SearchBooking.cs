using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class SearchBooking
    {
        public Flight flight { get; set; }
        public Booking booking { get; set; }
        public List<Flight> flights { get; set; }
    }
}