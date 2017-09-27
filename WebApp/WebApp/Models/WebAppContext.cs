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
        public WebAppContext() : base("WebApp")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<FlightRoutes> FlightRoutes { get; set; }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<AirportFlights> AirportFlights { get; set; }
        public DbSet<Airports> Airports { get; set; }
    }
    public class Cities
    {
        [Key]
        public String ZipCode { get; set; }
        public String City { get; set; }
        public virtual List<Customers> Customers { get; set; }
    }
    public class Customers
    {
        [Key]
        public int ID { get; set; }
        public int BookingsID { get; set; }
        public String Address { get; set; }
        public String ZipCode { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String EMail { get; set; }
        public bool ContactPerson { get; set; }
        public virtual Bookings Booking { get; set; }
        public virtual Cities Cities { get; set; }
    }
    public class Bookings
    {
        [Key]
        public int ID { get; set; }
        public int TravelFlightID { get; set; }
        public int ReturnFlightID { get; set; }
        public int Travelers { get; set; }
        public bool RoundTrip { get; set; }

        public virtual List<Customers> Customers { get; set; }
        public virtual FlightRoutes FlightRoutes { get; set; }
    }
    public class FlightRoutes
    {
        [Key]
        public int ID { get; set; }
        public int BookingsID { get; set; }
        public int FlightsID { get; set; }
        public virtual List<Bookings> Bookings { get; set; }
        public virtual List<Flights> Flights { get; set; }
    }
    public class Flights
    {
        [Key]
        public int ID { get; set; }

        public String TravelDate { get; set; }

        public int Departure { get; set; }

        public String DepartureTime { get; set; }

        public int Destination { get; set; }

        public String DestinationTime { get; set; }

        public String ClassType { get; set; }

        public virtual FlightRoutes FlightRoute { get; set; }
        public virtual AirportFlights AirportFlights { get; set; }
    }
    public class AirportFlights
    {
        [Key]
        public int ID { get; set; }
        public int FlightsID { get; set; }
        public int AirportsID { get; set; }
        public virtual List<Flights> Flights { get; set; }
        public virtual List<Airports> Airports { get; set; }
    }
    public class Airports
    {
        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public virtual AirportFlights AirportFlights { get; set; }
    }
}