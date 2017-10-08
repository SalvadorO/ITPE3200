using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewCustomer
    {
        public int id { get; set; }
        public int bookingId { get; set; }
        public bool contactPerson { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(pattern: @"[A-Za-z\S]{2,}", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String firstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(pattern: @"[A-Za-z\S]{2,}", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String lastName { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Postummer må være 8 siffer")]
        public String phoneNumber { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        [RegularExpression(pattern: @"[A-Za-z0-9\s\S]{4,}", ErrorMessage = "Adresse må fylles ut")]
        public String address { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postummer må være 4 siffer")]
        public String zipCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        [RegularExpression(pattern: @"[A-Za-z\s]{2,}", ErrorMessage = "Kun bokstaver, minimum 3")]
        public String city { get; set; }

        [Display(Name = "E-Post")]
        [Required(ErrorMessage = "E-Post må oppgis")]
        [EmailAddress(ErrorMessage ="E-post på være på formen: eksempel@eksempel.com")]
        public String eMail { get; set; }
    }
    

}