using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace WebApp.DAL
{
    public class WAPPContext : DbContext
    {
        public WAPPContext() : base("Database")
        {
            Database.CreateIfNotExists();
        }


        public override int SaveChanges()
        {

            var changedList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
            foreach(var x in changedList){
                foreach (var y in GetChanges(x)) {
                    ChangeLog.Add(y);
                }
            }
            return base.SaveChanges();
        }


        private List<ChangeLog> GetChanges(DbEntityEntry dbEntry)
        {
            List<ChangeLog> result = new List<ChangeLog>();

            DateTime changeTime = DateTime.UtcNow;

            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() as TableAttribute;

            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

            if (dbEntry.State == EntityState.Added)
            {
                foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    result.Add(new ChangeLog()
                    {
                        DateChanged = changeTime,
                        EntityType = "Add",
                        TableName = tableName,
                        RecordId = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),
                        ColumnName = propertyName,
                        NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                    }
                            );
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                result.Add(new ChangeLog()
                {
                    DateChanged = changeTime,
                    EntityType = "Delete",
                    TableName = tableName,
                    RecordId = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                    ColumnName = "ALL",
                    NewValue = dbEntry.OriginalValues.ToObject().ToString()
                }
                    );
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        result.Add(new ChangeLog()
                        {
                            EntityType = "Modified",
                            DateChanged = changeTime,
                            TableName  = tableName,
                            RecordId = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                            ColumnName = propertyName,
                            OldValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                            NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                        }
                            );
                    }
                }
            }

            return result;
        }





        public DbSet<City> City { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<Shadow_DB> Shadow { get; set; }
        public DbSet<Employee_DB> Employee { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<ChangeLog> ChangeLog { get; set; }
    }


    public class ChangeLog
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateChanged { get; set; }
        public String EntityType { get; set; }
        public String TableName { get; set; }
        public String RecordId { get; set; }
        public String ColumnName { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
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
        public virtual Booking Booking { get; set; }
        public virtual City Cities { get; set; }
    }
    public class Booking
    {
        [Key]
        public int ID { get; set; }
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
        public virtual List<Booking> Booking { get; set; }
        public virtual List<Airport> Airports { get; set; }
        public virtual Airplane Airplane { get; set; }
    }
    public class Airport
    {
        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public String Country { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
    public class Airplane
    {
        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public int Seats { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}
