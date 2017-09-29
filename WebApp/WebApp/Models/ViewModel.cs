using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewModel
    {
       public ViewBooking booking { get; set; }
       public List<ViewCustomer> customers { get; set; }
       public ViewFlight flight { get; set; }
       public List<List<ViewFlight>> travelflights { get; set; }
        public List<List<ViewFlight>> returnflights { get; set; }
    }
}