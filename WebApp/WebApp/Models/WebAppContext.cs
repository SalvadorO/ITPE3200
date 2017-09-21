using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class WebAppContext : DbContext
    {
        public WebAppContext() : base("name=WebApp")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Customers> Customers { get; set; }
    }
    public class Customers
    {
        [Key]
        public int CID { get; set; }
        public int BID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String EMail { get; set; }
        public bool ContactPerson { get; set; }
        public virtual Bookings Booking { get; set; }
        //public virtual Cities Cities { get; set; }
    }
    public class Bookings
    {
        [Key]
        public int BID { get; set; }
        public String Departure { get; set; }
        public String Destination { get; set; }
        public String TravelDate { get; set; }
        public String ReturnDate { get; set; }
        public String ClassType { get; set; }
        public int Travelers { get; set; }
        public bool OneWay { get; set; }
        public virtual List<Customers> Customers { get; set; }
    }

    /*public class Cities
    {
        public String ZipCode { get; set; }
        public String City { get; set; }
        public virtual List<Customers> Customers { get; set; }
    }*/
}