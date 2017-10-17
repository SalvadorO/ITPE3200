using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Employee
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Fornavn")]
        public String FirstName { get; set; }
        [Display(Name = "Etternavn")]
        public String LastName { get; set; }
        [Display(Name = "Telefonnummer")]
        public String PhoneNumber { get; set; }
        [Display(Name = "E-post")]
        public String EMail { get; set; }
        [Display(Name = "Adresse")]
        public String Address { get; set; }
        [Display(Name = "Postnummer")]
        public String ZipCode { get; set; }
        [Display(Name = "Poststed")]
        public String City { get; set; }
    }
}
