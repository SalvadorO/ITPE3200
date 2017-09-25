using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Airport
    {
        [Key]
        public int id { get; set; }
        public String name { get; set; }
    }
}