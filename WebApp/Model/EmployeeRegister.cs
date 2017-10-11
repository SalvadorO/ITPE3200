using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAdmin.Model
{
    public class EmployeeRegister
    {
        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string Username { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Password { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public String Address { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        public String ZipCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public String City { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public String FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public String LastName { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        public String PhoneNumber { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "E-post må oppgis")]
        public String EMail { get; set; }
    }
}
