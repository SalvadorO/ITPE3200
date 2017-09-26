using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Flight
    {
        public int id { get; set; }
        [Display(Name = "Fra tid")]
        public String departureTime { get; set; }
        [Display(Name = "Til tid")]
        public String destinationTime { get; set; }

        [Display(Name = "Fra")]
        [Required(ErrorMessage = "Fra må oppgis")]
        public String departure { get; set; }

        [Display(Name = "Til")]
        [Required(ErrorMessage = "Til må oppgis")]
        public String destination { get; set; }

        [Display(Name = "Dato ut")]
        [Required(ErrorMessage = "Dato ut må oppgis")]
        public String travelDate { get; set; }

        [Display(Name = "Dato hjem")]
        [Required(ErrorMessage = "Dato hjem må oppgis")]
        public String returnDate { get; set; }

        [Display(Name = "Klasse")]
        [Required(ErrorMessage = "Klasse må oppgis")]
        public String classType { get; set; }
    }
}