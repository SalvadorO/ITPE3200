using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewCardInfo
    {
        [Display(Name = "Kortholders navn")]
        [Required(ErrorMessage = "Kortholders navn må oppgis")]
        public String cardName { get; set; }

        [Display(Name = "Kortnummer")]
        [Required(ErrorMessage = "Kortnummer må oppgis")]
        public String cardNr { get; set; }

        [Display(Name = "3-siffer-kode")]
        [Required(ErrorMessage = "3-siffer-kode må oppgis")]
      
        [RegularExpression(pattern: @"^[0-9]$" , ErrorMessage = "Må være Tall")]

        public String code { get; set; }
        

        [Display(Name = "Utløpsdato")]
        [Required(ErrorMessage = "Utløpsdato må oppgis")]
        public String expDateMM { get; set; }
        public String expDateYY { get; set; }
    }
}