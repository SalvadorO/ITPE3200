using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class EmployeeEditLogin
    {
        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string Username { get; set; }

        [Display(Name = "Gammelt passord")]
        [Required(ErrorMessage = "Gammelt passord må oppgis")]
        public string OldPassword { get; set; }

        [Display(Name = "Nytt Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Password { get; set; }

        [Display(Name = "Bekreft passord")]
        [Required(ErrorMessage = "Bekreft passord må oppgis")]
        [Compare("Password", ErrorMessage = "Passord må være like")]
        public string ConfirmPassword { get; set; }
    }
}
