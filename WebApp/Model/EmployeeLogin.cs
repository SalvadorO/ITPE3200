using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class EmployeeLogin
    {
        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string Username { get; set; }
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Password { get; set; }
    }
}
