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
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{3,}$", ErrorMessage = "minimum 3. Kan være både tall og bokstaver")]
        public string Username { get; set; }

        [Display(Name = "Gammelt passord")]
        [Required(ErrorMessage = "Gammelt passord må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{5,}$", ErrorMessage = "minimum 5. Kan være både tall og bokstaver")]
        public string OldPassword { get; set; }

        [Display(Name = "Nytt Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{5,}$", ErrorMessage = "minimum 5. Kan være både tall og bokstaver")]
        public string Password { get; set; }

        [Display(Name = "Bekreft passord")]
        [Required(ErrorMessage = "Bekreft passord må oppgis")]
        [Compare("Password", ErrorMessage = "Passord må være like")]
        public string ConfirmPassword { get; set; }
    }
}
