using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminFlight
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Avgang")]
        [Required(ErrorMessage = "Avgang må oppgis")]
        public String DepartureTime { get; set; }
        [Display(Name = "Ankomst")]
        [Required(ErrorMessage = "Ankomst må oppgis")]
        public String DestinationTime { get; set; }
        [Display(Name = "Fra")]
        [Required(ErrorMessage = "Fra må oppgis")]
        public String Departure { get; set; }
        [Display(Name = "Til")]
        [Required(ErrorMessage = "Til må oppgis")]
        public String Destination { get; set; }
        [Display(Name = "Reisedato")]
        [Required(ErrorMessage = "Reisedato må oppgis")]
        public String TravelDate { get; set; }
        [Display(Name = "Klasse")]
        [Required(ErrorMessage = "Klasse må oppgis")]
        public String ClassType { get; set; }
        [Display(Name = "Ledige plasser")]
        [Required(ErrorMessage = "Ledige plasser må oppgis")]
        public int Seats { get; set; }
        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Pris må oppgis")]
        public int Price { get; set; }
    }
}
