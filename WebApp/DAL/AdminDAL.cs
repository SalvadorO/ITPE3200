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
                var newEmp = new Employee();
                var newlogin = new Shadow();
                    string salt = Salt();
                    var passwordNSalt = inEmp.Password + salt;
                    byte[] passwordDB = Hash(passwordNSalt);
                    newEmp.FirstName = inEmp.FirstName;
                    newEmp.LastName = inEmp.LastName;
                    newEmp.Address = inEmp.Address;
                    newEmp.ZipCode = inEmp.ZipCode;
                    newEmp.EMail = inEmp.EMail;
                    newEmp.PhoneNumber = inEmp.PhoneNumber;
                    newlogin.Username = inEmp.Username;
                    newlogin.Password = passwordDB;
                    newlogin.Salt = salt;

                    var existingZip = db.City.Find(inEmp.ZipCode);
                    if (existingZip == null)
                    {
                    var newCity = new City()
                    {
                        ZipCode = inEmp.ZipCode,
                        CityName = inEmp.City
                    };
                    newEmp.Cities = newCity;
                    }
                    db.Employee.Add(newEmp);
                    db.Shadow.Add(newlogin);
                    db.SaveChanges();
                    return true;

                    }catch(Exception error)
                    {
                        return false;
                    }
            }
        }
    }
}
