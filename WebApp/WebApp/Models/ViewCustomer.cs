﻿using System;
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
        public String firstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public String lastName { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        public String phoneNumber { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public String address { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postummer må være 4 siffer")]
        public String zipCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public String city { get; set; }

        [Display(Name = "E-Post")]
        [Required(ErrorMessage = "E-Post må oppgis")]
        [EmailAddress(ErrorMessage ="E-post på være på formen: eksempel@eksempel.com")]
        public String eMail { get; set; }

        [Display(Name = "Kortholders navn")]
        [Required(ErrorMessage = "Kortholders navn må oppgis")]
        public String cardName { get; set; }

        [Display(Name = "Kortnummer")]
        [Required(ErrorMessage = "Kortnummer må oppgis")]
        public String cardNr { get; set; }

        [Display(Name = "3-siffer-kode")]
        [Required(ErrorMessage = "3-siffer-kode må oppgis")]
        public String code { get; set; }

        [Display(Name = "Utløpsdato")]
        [Required(ErrorMessage = "Utløpsdato må oppgis")]
        public String expDate { get; set; }
    }
    

}