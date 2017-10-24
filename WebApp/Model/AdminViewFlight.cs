using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminViewFlight
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Avgang")]
        public String DepartureTime { get; set; }
        [Display(Name = "Ankomst")]
        public String DestinationTime { get; set; }
        [Display(Name = "Fra")]
        public String Departure { get; set; }
        [Display(Name = "Til")]
        public String Destination { get; set; }
        [Display(Name = "Reisedato")]
        public String TravelDate { get; set; }
        [Display(Name = "Klasse")]
        public String ClassType { get; set; }
        [Display(Name = "Flymaskin")]
        public String Airplane { get; set; }
        [Display(Name = "Ledige plasser")]
        public int Seats { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }
        public int BookingID { get; set; }
    }
}
