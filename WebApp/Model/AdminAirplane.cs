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
        public String Name { get; set; }
        [Display(Name = "Seter")]
        [Required(ErrorMessage = "Seter må oppgis")]
        public int Seats { get; set; }
    }
}
