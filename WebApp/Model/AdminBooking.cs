using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminBooking
    {
        public List<AdminViewFlight> Flights { get; set; }
    }
}
