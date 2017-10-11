using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAdmin.DAL
{
    class CustomerDBContext : DbContext
    {
        public CustomerDBContext() : base("WebApp")
        {
            Database.CreateIfNotExists();
        }
    }
}
