using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminCustomer
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-zÆØÅæøå \s]{3,}$", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String FirstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-zÆØÅæøå \s]{3,}$", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String LastName { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(pattern: @"[0-9]{8}", ErrorMessage = "Må være 8 siffer")]
        public String PhoneNumber { get; set; }

        [Display(Name = "E-Post")]
        public String EMail { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-z0-9ÆØÅæøå \s]{3,}$", ErrorMessage = "minimum 3 bokstaver")]
        public String Address { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(pattern: @"[0-9]{4}", ErrorMessage = "Må være 4 siffer")]
        public String ZipCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        [RegularExpression(pattern: @"^[A-Za-zÆØÅæøå \s]{2,}$", ErrorMessage = "Kun bokstaver, minimum 2")]
        public String City { get; set; }

        [Display(Name = "Kontaktperson")]
        public bool ContactPerson { get; set; }
    }
}
