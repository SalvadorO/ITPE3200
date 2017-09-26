using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Booking
    {
        public int id { get; set; }
        public int flightId { get; set; }
        public int returnFlightId { get; set; }
        public bool oneWay { get; set; }

        [Display(Name = "Antall reisende")]
        [Required(ErrorMessage = "Antall reisende må oppgis")]
        public int travelers { get; set; }
    }
}