using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Customer
    {
        [Key]
        public int cId { get; set; }
        public int bId { get; set; }
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

        [Display(Name = "E-Post")]
        [Required(ErrorMessage = "E-Post må oppgis")]
        public String eMail { get; set; }
    }
    

}