using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApp.DAL
{
        public class WAPPContext : DbContext
        {
            public WAPPContext() : base("Database")
            {
                Database.CreateIfNotExists();
            }
            public DbSet<City> City { get; set; }
            public DbSet<Customer> Customer { get; set; }
            public DbSet<Booking> Booking { get; set; }
            public DbSet<Flight> Flight { get; set; }
            public DbSet<Airport> Airport { get; set; }
            public DbSet<Shadow_DB> Shadow { get; set; }
            public DbSet<Employee_DB> Employee { get; set; }
        }

        public class Shadow_DB
        {
            [Key, ForeignKey("Employee")]
            public int Employee_ID { get; set; }
            public string Username { get; set; }
            public byte[] Password { get; set; }
            public string Salt { get; set; }
            public virtual Employee_DB Employee { get; set; }
        }
        public class Employee_DB
        {
                [Key]
                public int ID { get; set; }
                public String FirstName { get; set; }
                public String LastName { get; set; }
                public String PhoneNumber { get; set; }
                public String EMail { get; set; }
                public String Address { get; set; }
                public String ZipCode { get; set; }
                public virtual City City { get; set; }
                public virtual Shadow_DB Shadow { get; set; }
        }
        public class City
        {
                [Key]
                public String ZipCode { get; set; }
                public String CityName { get; set; }
                public virtual List<Customer> Customers { get; set; }
                public virtual List<Employee_DB> Employees { get; set; }

        }
        public class Customer
        {
                [Key]
                public int ID { get; set; }
                public String Address { get; set; }
                public String ZipCode { get; set; }
                public String FirstName { get; set; }
                public String LastName { get; set; }
                public String PhoneNumber { get; set; }
                public String EMail { get; set; }
                public bool ContactPerson { get; set; }
                public virtual List<Booking> Bookings { get; set; }
                public virtual City Cities { get; set; }
        }
        public class Booking
        {
                [Key]
                public int ID { get; set; }
                public int FlightID { get; set; }
                public int Travelers { get; set; }
                public bool RoundTrip { get; set; }
                public String CardName { get; set; }
                public String CardNr { get; set; }
                public String Code { get; set; }
                public String ExpDate { get; set; }
                public virtual List<Customer> Customers { get; set; }
                public virtual List<Flight> Flights { get; set; }
        }
        public class Flight
        {
                [Key]
                public int ID { get; set; }
                public String TravelDate { get; set; }
                public int Departure { get; set; }
                public String DepartureTime { get; set; }
                public int Destination { get; set; }
                public String DestinationTime { get; set; }
                public String ClassType { get; set; }
                public int Price { get; set; }
                public int Seats { get; set; }
                public virtual List<Booking> Bookings { get; set; }
                public virtual List<Airport> Airports { get; set; }
        }
         public class Airport
         {
                [Key]
                public int ID { get; set; }
                public String Name { get; set; }
                public String Country { get; set; }
                public virtual List<Flight> Flights { get; set; }
         }
}
