using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class FinalBooking
    {
       public Booking booking { get; set; }
       public List<Customer> customers { get; set; }
       public Flight flight { get; set; }
    }
}