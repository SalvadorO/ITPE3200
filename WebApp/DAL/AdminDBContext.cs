using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAdmin.DAL
{
    public class AdminDBContext : DbContext
    {
        public AdminDBContext() : base("admin")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Shadow> Shadow { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<City> City { get; set; }
    }

    public class Shadow
    {
        [Key]
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string Salt { get; set; }
    }

    public class City
    {
        [Key]
        public String ZipCode { get; set; }
        public String CityName { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String EMail { get; set; }
        public String Address { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public virtual City Cities { get; set; }
    }
}
