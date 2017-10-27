using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminAirport
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-zÆØÅæøå \s]{3,}$", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String Name { get; set; }
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Land må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-zÆØÅæøå \s]{3,}$", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String Country { get; set; }
    }
}
