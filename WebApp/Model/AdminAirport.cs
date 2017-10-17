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
        public String Name { get; set; }
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Land må oppgis")]
        public String Country { get; set; }
    }
}
