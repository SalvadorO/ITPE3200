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
        public String firstName { get; set; }
        public String lastName { get; set; }
        public virtual Booking Booking { get; set; }
    }
    

}