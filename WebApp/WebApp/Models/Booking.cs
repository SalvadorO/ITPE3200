using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Booking
    {
        [Key]
        public int bId { get; set; }
        public bool oneWay { get; set; }

        [Display(Name = "Fly fra")]
        [Required(ErrorMessage = "Fly fra må oppgis")]
        public String departure { get; set; }

        [Display(Name = "Fly til")]
        [Required(ErrorMessage = "Fly til må oppgis")]
        public String destination { get; set; }

        [Display(Name = "Utreise")]
        [Required(ErrorMessage = "Utreise må oppgis")]
        public String travelDate { get; set; }

        [Display(Name = "Retur")]
        [Required(ErrorMessage = "Retur må oppgis")]
        public String returnDate { get; set; }

        [Display(Name = "Klasse")]
        [Required(ErrorMessage = "Klasse må oppgis")]
        public String classType { get; set; }

        [Display(Name = "Reisende")]
        [Required(ErrorMessage = "Reisende må oppgis")]
        public int travelers { get; set; }
    }
}