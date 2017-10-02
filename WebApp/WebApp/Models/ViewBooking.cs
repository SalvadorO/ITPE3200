using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewBooking
    {
        public int id { get; set; }
        public int flightId { get; set; }
        public int returnFlightId { get; set; }

        [Display(Name = "Tur/Retur")]
        public bool roundTrip { get; set; }

        [Display(Name = "Antall reisende")]
        [Required(ErrorMessage = "Antall reisende må oppgis")]
        public int travelers { get; set; }
        public int price { get; set; }

        public List<ViewFlight> chosenTravel { get; set; }
        public List<ViewFlight> chosenReturn { get; set; }
    }
}