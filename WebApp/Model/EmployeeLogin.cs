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
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{3,}$", ErrorMessage = "Brukernavnet er for kort. Må være minimum 3")]

        public string Username { get; set; }
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{5,}$", ErrorMessage = "Passordet er for kort. Må være minimum 5")]
        public string Password { get; set; }
    }
}
