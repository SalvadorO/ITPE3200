using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminAirplane
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{3,}$", ErrorMessage = "minimum 3 tall eller bokstaver")]
        public String Name { get; set; }
        [Display(Name = "Seter")]
        [Required(ErrorMessage = "Seter må oppgis")]
        [RegularExpression(pattern: @"^[0-9]{2,}", ErrorMessage = "Må være større enn to siffer")]
        public int Seats { get; set; }
    }
}
