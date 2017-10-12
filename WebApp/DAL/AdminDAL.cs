using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppAdmin.Model;

namespace WebAppAdmin.DAL
{
    public class AdminDAL
    {
        //Kode brukt fra forelesning
        public bool getShadow(EmployeeLogin inEmp)
        {
            using (var db = new AdminDBContext())
            {
                var foundEmp = db.Shadow.FirstOrDefault(b => b.Username == inEmp.Username);
                if (foundEmp != null)
                {
                    byte[] passwordForTest = Hash(inEmp.Password + foundEmp.Salt);
                    bool rightEmp = foundEmp.Password.SequenceEqual(passwordForTest);
                    return rightEmp;
                }
                else
                {
                    return false;
                }
            }
        }

        //Kode brukt fra forelesning
        private byte[] Hash(string inString)
        {
            byte[] inData, outData;
            var algorithm = SHA256.Create();
            inData = Encoding.UTF8.GetBytes(inString);
            outData = algorithm.ComputeHash(inData);
            return outData;
        }

        //Kode brukt fra forelesning
        private string Salt()
        {
            byte[] randomArray = new byte[10];
            string randomString;

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

        public bool insertEmployee(EmployeeRegister inEmp)
        {
            using (var db = new AdminDBContext())
            {
                try { 
                var newEmp = new Employee_DB();
                var newShadow = new Shadow_DB();
                    string salt = Salt();
                    var passwordNSalt = inEmp.Password + salt;
                    byte[] passwordDB = Hash(passwordNSalt);
                    newEmp.FirstName = inEmp.FirstName;
                    newEmp.LastName = inEmp.LastName;
                    newEmp.Address = inEmp.Address;
                    newEmp.ZipCode = inEmp.ZipCode;
                    newEmp.EMail = inEmp.EMail;
                    newEmp.PhoneNumber = inEmp.PhoneNumber;
                    newShadow.Username = inEmp.Username;
                    newShadow.Password = passwordDB;
                    newShadow.Salt = salt;
                    newEmp.Shadow = newShadow;
                    var existingZip = db.City.Find(inEmp.ZipCode);
                    if (existingZip == null)
                    {
                    var newCity = new City_DB()
                    {
                        ZipCode = inEmp.ZipCode,
                        CityName = inEmp.City
                    };
                    newEmp.City = newCity;
                    }
                    db.Employee.Add(newEmp);
                    db.SaveChanges();
                    return true;

                    }catch(Exception error)
                    {
                        return false;
                    }
            }
        }

        public List<Employee> listEmployee()
        {
            var db = new AdminDBContext();
            List<Employee> all = db.Employee.Select(e => new Employee()
            {
                ID = e.ID,
                FirstName = e.FirstName,
                LastName = e.LastName
            }
            ).ToList();
            return all;
        }

        public Employee oneEmployee(int id)
        {
            var db = new AdminDBContext();
            var e = db.Employee.Find(id);

            if (db.Employee.Find(id) == null)
            {
                return null;
            }
            else {

                var one = new Employee()
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    EMail = e.EMail,
                    Address = e.Address,
                    ZipCode = e.ZipCode,
                    City = e.City.CityName
                };

                return one;
            }
        }

        public bool editEmployee(int id, Employee inEmp)
        {
            var db = new AdminDBContext();
            try
            {
                var editEmp = db.Employee.Find(id);
                editEmp.FirstName = inEmp.FirstName;
                editEmp.LastName = inEmp.LastName;
                editEmp.PhoneNumber = inEmp.PhoneNumber;
                editEmp.EMail = inEmp.EMail;
                editEmp.Address = inEmp.Address;
                if(editEmp.ZipCode != inEmp.ZipCode)
                {
                    if(db.City.FirstOrDefault(z => z.ZipCode == inEmp.ZipCode) == null)
                    {
                        var newCity = new City_DB()
                        {
                            ZipCode = inEmp.ZipCode,
                            CityName = inEmp.City
                        };
                        db.City.Add(newCity);
                    }
                editEmp.ZipCode = inEmp.ZipCode;
                }
                db.SaveChanges();
                return true;
            }catch(Exception error)
            {
                return false;
            }
        }

        public bool deleteEmployee(int id)
        {
            var db = new AdminDBContext();
            try
            {
                db.Shadow.Remove(db.Shadow.Find(id));
                db.Employee.Remove(db.Employee.Find(id));
                db.SaveChanges();
                return true;
            }
            catch(Exception error)
            {
                return false;
            }

        }

        public int getUsernameID(String uname)
        {
            return new AdminDBContext().Shadow.Where(w => w.Username.Equals(uname))
                .Select(s => s.Employee_ID).FirstOrDefault();
        }
    }
}
