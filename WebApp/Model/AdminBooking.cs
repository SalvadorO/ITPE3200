using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AdminBooking
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name="Kontaktperson")]
        public String ContactName { get; set; }
        [Display(Name = "Reise")]
        public String RTString { get; set; }
        [Display(Name = "Antall reisende")]
        public int Travelers { get; set; }
        [Display(Name = "Reisedato")]
        public String TravelDate { get; set; }
        [Display(Name = "Returdato")]
        public String ReturnDate { get; set; }
        public int cID { get; set; }
    }
}
