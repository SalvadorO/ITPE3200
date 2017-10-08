using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewFlight
    {
        public int id { get; set; }
        [Display(Name = "Avgang")]
        public String departureTime { get; set; }
        [Display(Name = "Ankomst")]
        public String destinationTime { get; set; }

        [Display(Name = "Fra")]
        [Required(ErrorMessage = "Fra må oppgis")]
        public String departure { get; set; }

        [Display(Name = "Til")]
        [Required(ErrorMessage = "Til må oppgis")]
        public String destination { get; set; }

        [Display(Name = "Reisedato")]
        [Required(ErrorMessage = "Dato ut må oppgis")]
        public String travelDate { get; set; }

        [Display(Name = "Returdato")]
        [Required(ErrorMessage = "Dato hjem må oppgis")]
        public String returnDate { get; set; }

        [Display(Name = "Klasse")]
        [Required(ErrorMessage = "Klasse må oppgis")]
        public String classType { get; set; }

        [Display(Name = "Ledige plasser")]
        public int seats { get; set; }
        [Display(Name = "Pris")]
        public int price { get; set; }

        public List<List<int>> travelIDs { get; set; }
        public List<List<int>> returnIDs { get; set; }
    }
}