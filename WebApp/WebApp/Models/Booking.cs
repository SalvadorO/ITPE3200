using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Booking
    {
        [Key]
        public int bId { get; set; }
        public String departure { get; set; }
        public String destination { get; set; }
        public String travelDate { get; set; }
        public String returnDate { get; set; }
        public String classType { get; set; }
        public int adult { get; set; }
        public int child { get; set; }
        public bool oneWay { get; set; }
        public virtual List<Customer> Customers { get; set; }
    }
}