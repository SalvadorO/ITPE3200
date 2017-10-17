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
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Password { get; set; }
    }
}
