﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApp.Model;
using System.Web;

namespace WebApp.DAL
{
    public class AdminDAL : IAdminRepository
    {
        public bool getShadow(EmployeeLogin inEmp)
        {
            using (var db = new WAPPContext())
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

        private byte[] Hash(string inString)
        {
            byte[] inData, outData;
            var algorithm = SHA256.Create();
            inData = Encoding.UTF8.GetBytes(inString);
            outData = algorithm.ComputeHash(inData);
            return outData;
        }

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
            using (var db = new WAPPContext())
            {
                try
                {
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
                        var newCity = new City()
                        {
                            ZipCode = inEmp.ZipCode,
                            CityName = inEmp.City
                        };
                        newEmp.City = newCity;
                    }
                    db.Employee.Add(newEmp);
                    db.SaveChanges();
                    return true;

                }
                catch (Exception exception)
                {
                    LogException(exception);
                    return false;
                }
            }
        }

        public List<Employee> listEmployee()
        {
            var db = new WAPPContext();
            List<Employee> all = db.Employee.Select(e => new Employee()
            {
                ID = e.ID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Username = e.Shadow.Username
            }
            ).ToList();
            return all;
        }

        public Employee oneEmployee(int id)
        {
            var db = new WAPPContext();
            var e = db.Employee.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {

                var one = new Employee()
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    EMail = e.EMail,
                    Address = e.Address,
                    ZipCode = e.ZipCode,
                    City = e.City.CityName,
                    Username = e.Shadow.Username
                };

                return one;
            }
        }

        public AdminBooking oneBooking(int id)
        {
            var db = new WAPPContext();
            var s = db.Booking.Find(id);

            if (s == null)
            {
                return null;
            }
            else
            {

                var one = new AdminBooking()
                {
                    ContactName = s.Customers.FirstOrDefault().FirstName + " " + s.Customers.FirstOrDefault().LastName,
                    cID = s.Customers.FirstOrDefault().ID,
                    ID = s.ID,
                    RTString = s.RoundTrip.ToString(),
                    Travelers = s.Travelers,
                    TravelDate = s.Flights.FirstOrDefault().TravelDate
                };

                String t = one.RTString;
                one.RTString = roundtripToString(t);

                return one;
            }
        }

        public AdminCustomer oneCustomer(int id)
        {
            var db = new WAPPContext();
            var e = db.Customer.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {

                var one = new AdminCustomer()
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    EMail = e.EMail,
                    Address = e.Address,
                    ZipCode = e.ZipCode,
                    City = e.Cities.CityName,
                    ContactPerson = e.ContactPerson
                };

                return one;
            }
        }

        public AdminCustomer searchCustomer(int id)
        {
            var db = new WAPPContext();
            var e = db.Customer.Find(id);

            if (e == null || e.ContactPerson == false)
            {
                return null;
            }
            else
            {

                var one = new AdminCustomer()
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    EMail = e.EMail,
                    Address = e.Address,
                    ZipCode = e.ZipCode,
                    City = e.Cities.CityName,
                    ContactPerson = e.ContactPerson
                };
                return one;
            }
        }

        public bool editCustomer(int id, AdminCustomer inCust)
        {
            var db = new WAPPContext();
            try
            {
                var editCust = db.Customer.Find(id);
                editCust.FirstName = inCust.FirstName;
                editCust.LastName = inCust.LastName;
                editCust.PhoneNumber = inCust.PhoneNumber;
                editCust.EMail = inCust.EMail;
                editCust.Address = inCust.Address;
                if (editCust.ZipCode != inCust.ZipCode)
                {
                    if (db.City.FirstOrDefault(z => z.ZipCode == inCust.ZipCode) == null)
                    {
                        var newCity = new City()
                        {
                            ZipCode = inCust.ZipCode,
                            CityName = inCust.City
                        };
                        db.City.Add(newCity);
                    }
                    editCust.ZipCode = inCust.ZipCode;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public bool deleteCustomer(int id)
        {
            
            var db = new WAPPContext();
            try
            {
                var c = db.Customer.Find(id);
                if (c.ContactPerson == true)
                {
                    foreach (var item in c.Booking.Flights)
                    {
                        item.Seats += c.Booking.Travelers;
                    }
                    int t = c.Booking.ID;
                    var list = db.Customer.Where(w => w.Booking.ID == t).ToList();
                    foreach (var i in list)
                    {
                        db.Customer.Remove(i);
                    }
                    db.Booking.Remove(db.Booking.Find(t));
                }
                else
                {
                    c.Booking.Travelers--;
                    foreach (var item in c.Booking.Flights)
                    {
                        item.Seats++;
                    }
                    db.Customer.Remove(c);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public List<AdminViewFlight> customerBooking(int id)
        {
            var db = new WAPPContext();
            var cp = db.Customer.Find(id);
            List<AdminViewFlight> list = cp.Booking.Flights.Select(e => new AdminViewFlight()
            {
                ID = e.ID,
                TravelDate = e.TravelDate,
                DepartureTime = e.DepartureTime,
                Departure = db.Airport.Where(w => w.ID == e.Departure).Select(s => s.Name).FirstOrDefault(),
                DestinationTime = e.DestinationTime,
                Destination = db.Airport.Where(w => w.ID == e.Destination).Select(s => s.Name).FirstOrDefault(),
                ClassType = e.ClassType,
                Airplane = e.Airplane.Name,
                Seats = e.Seats,
                Price = e.Price,
                BookingID = id
            }).OrderBy(o => o.TravelDate).ThenBy(o => o.DepartureTime).ToList();

            return list;
        }

        public List<AdminCustomer> detailCustomer(int id)
        {
            var db = new WAPPContext();
            var CP = oneCustomer(id);
            var cp = db.Customer.Find(id);
            List<AdminCustomer> list = db.Customer.Where(w => w.Booking.ID == cp.Booking.ID
            && w.ContactPerson == false).Select(e => new AdminCustomer()
            {
                ID = e.ID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumber = e.PhoneNumber,
                EMail = e.EMail,
                Address = e.Address,
                ZipCode = e.ZipCode,
                City = e.Cities.CityName

            }).ToList();
            list.Insert(0, CP);
            return list;
        }

        public bool editEmployee(int id, Employee inEmp)
        {
            var db = new WAPPContext();
            try
            {
                var editEmp = db.Employee.Find(id);
                editEmp.FirstName = inEmp.FirstName;
                editEmp.LastName = inEmp.LastName;
                editEmp.PhoneNumber = inEmp.PhoneNumber;
                editEmp.EMail = inEmp.EMail;
                editEmp.Address = inEmp.Address;
                if (editEmp.ZipCode != inEmp.ZipCode)
                {
                    if (db.City.FirstOrDefault(z => z.ZipCode == inEmp.ZipCode) == null)
                    {
                        var newCity = new City()
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
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public Employee searchEmployee(String uname)
        {
            var db = new WAPPContext();
            int id = db.Shadow.Where(w => w.Username.Equals(uname)).Select(s => s.Employee_ID).FirstOrDefault();
            return oneEmployee(id);
        }

        public AdminBooking searchBooking(int id)
        {
            return oneBooking(id);
        }

        public bool deleteEmployee(int id)
        {
            var db = new WAPPContext();
            try
            {
                db.Shadow.Remove(db.Shadow.Find(id));
                db.Employee.Remove(db.Employee.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }

        }
        public bool usernameExist(String uname)
        {
            if (new WAPPContext().Shadow.Where(w => w.Username.Equals(uname)).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int getUsernameID(String uname)
        {
            return new WAPPContext().Shadow.Where(w => w.Username.Equals(uname))
                .Select(s => s.Employee_ID).FirstOrDefault();
        }

        public String getUsername(int id)
        {
            return new WAPPContext().Shadow.Where(w => w.Employee_ID == id)
                .Select(s => s.Username).FirstOrDefault();
        }

        public bool correctOldPassword(int id, String op)
        {
            using (var db = new WAPPContext())
            {
                var foundEmp = db.Shadow.FirstOrDefault(b => b.Employee_ID == id);
                if (foundEmp != null)
                {
                    byte[] passwordForTest = Hash(op + foundEmp.Salt);
                    bool rightEmp = foundEmp.Password.SequenceEqual(passwordForTest);
                    return rightEmp;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool editEmployeeLogin(int id, EmployeeEditLogin inEEL)
        {
            using (var db = new WAPPContext())
            {
                try
                {
                    var editShadow = db.Shadow.Find(id);
                    string salt = Salt();
                    var passwordNSalt = inEEL.Password + salt;
                    byte[] passwordDB = Hash(passwordNSalt);
                    editShadow.Username = inEEL.Username;
                    editShadow.Password = passwordDB;
                    editShadow.Salt = salt;
                    db.SaveChanges();
                    return true;

                }
                catch (Exception exception)
                {
                    LogException(exception);
                    return false;
                }
            }
        }

        public List<AdminViewFlight> listAllFlights()
        {
            var db = new WAPPContext();
            List<AdminViewFlight> all = db.Flight.Select(e => new AdminViewFlight()
            {
                ID = e.ID,
                TravelDate = e.TravelDate,
                DepartureTime = e.DepartureTime,
                Departure = db.Airport.Where(w => w.ID == e.Departure).Select(s => s.Name).FirstOrDefault(),
                DestinationTime = e.DestinationTime,
                Destination = db.Airport.Where(w => w.ID == e.Destination).Select(s => s.Name).FirstOrDefault(),
                ClassType = e.ClassType,
                Airplane = e.Airplane.Name,
                Seats = e.Seats,
                Price = e.Price
            }
            ).ToList();
            return all;
        }

        public List<AdminCustomer> getPassengers(int id)
        {
            var db = new WAPPContext();
            List<AdminCustomer> list = new List<AdminCustomer>();
            var flight = db.Flight.Find(id);
            foreach (var x in flight.Booking)
            {
                foreach (var y in x.Customers)
                {
                    var temp = new AdminCustomer()
                    {
                        ID = y.ID,
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        ContactPerson = y.ContactPerson
                    };
                    list.Add(temp);
                }
            }
            return list;
        }

        public List<AdminCustomer> listContactPersons()
        {
            var db = new WAPPContext();
            List<AdminCustomer> contact = db.Customer.Where(w => w.ContactPerson == true).Select(e => new AdminCustomer()
            {
                ID = e.ID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                ContactPerson = e.ContactPerson
            }).ToList();
            return contact;
        }

        public AdminFlight oneFlight(int id)
        {
            var db = new WAPPContext();
            var e = db.Flight.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {

                var one = new AdminFlight()
                {
                    ID = e.ID,
                    TravelDate = e.TravelDate,
                    DepartureTime = e.DepartureTime,
                    Departure = e.Departure,
                    DestinationTime = e.DestinationTime,
                    Destination = e.Destination,
                    ClassType = e.ClassType,
                    Seats = e.Seats,
                    Airplane = e.Airplane.ID,
                    Price = e.Price
                };

                return one;
            }
        }

        public List<AdminViewFlight> searchFlight(SearchFlight search)
        {
            var db = new WAPPContext();
            int from = db.Airport.Where(w => w.Name.Equals(search.From)).Select(s => s.ID).FirstOrDefault();
            int to = db.Airport.Where(w => w.Name.Equals(search.To)).Select(s => s.ID).FirstOrDefault();
            List<AdminViewFlight> list = db.Flight.Where(w => w.Departure == from
            && w.Destination == to
            && w.TravelDate == search.Date).Select(e => new AdminViewFlight()
            {
                ID = e.ID,
                TravelDate = e.TravelDate,
                DepartureTime = e.DepartureTime,
                Departure = db.Airport.Where(w => w.Name.Equals(search.From)).Select(s => s.Name).FirstOrDefault(),
                DestinationTime = e.DestinationTime,
                Destination = db.Airport.Where(w => w.Name.Equals(search.To)).Select(s => s.Name).FirstOrDefault(),
                ClassType = e.ClassType,
                Seats = e.Seats,
                Airplane = db.Airplanes.Where(w => w.ID == e.Airplane.ID).Select(s => s.Name).FirstOrDefault(),
                Price = e.Price
            }).ToList();
            return list;
        }

        public bool insertFlight(AdminFlight inFlight)
        {
            using (var db = new WAPPContext())
            {
                try
                {
                    var outFlight = new Flight();
                    outFlight.TravelDate = inFlight.TravelDate;
                    outFlight.DepartureTime = inFlight.DepartureTime;
                    outFlight.Departure = inFlight.Departure;
                    outFlight.DestinationTime = inFlight.DestinationTime;
                    outFlight.Destination = inFlight.Destination;
                    outFlight.ClassType = inFlight.ClassType;
                    outFlight.Airplane = db.Airplanes.Find(inFlight.Airplane);
                    outFlight.Seats = outFlight.Airplane.Seats;
                    outFlight.Price = inFlight.Price;
                    db.Flight.Add(outFlight);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    return false;
                }
            }
        }

        public bool editFlight(int id, AdminFlight ef)
        {
            var db = new WAPPContext();
            try
            {
                var editf = db.Flight.Find(id);
                editf.TravelDate = ef.TravelDate;
                editf.DepartureTime = ef.DepartureTime;
                editf.Departure = ef.Departure;
                editf.DestinationTime = ef.DestinationTime;
                editf.Destination = ef.Destination;
                editf.ClassType = ef.ClassType;
                editf.Airplane = db.Airplanes.Find(ef.Airplane);
                editf.Price = ef.Price;

                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public bool deleteFlight(int id)
        {
            var db = new WAPPContext();
            try
            {
                db.Flight.Remove(db.Flight.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public List<AdminAirplane> listAirplanes()
        {
            var db = new WAPPContext();
            List<AdminAirplane> all = db.Airplanes.Select(e => new AdminAirplane()
            {
                ID = e.ID,
                Name = e.Name,
                Seats = e.Seats
            }
            ).ToList();
            return all;
        }

        public List<AdminAirport> listAirports()
        {
            var db = new WAPPContext();
            List<AdminAirport> all = db.Airport.Select(e => new AdminAirport()
            {
                ID = e.ID,
                Name = e.Name,
                Country = e.Country
            }
            ).ToList();
            return all;
        }
        public bool insertAirplane(AdminAirplane inAir)
        {
            using (var db = new WAPPContext())
            {
                try
                {
                    var newAir = new Airplane();
                    newAir.Name = inAir.Name;
                    newAir.Seats = inAir.Seats;
                    db.Airplanes.Add(newAir);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    return false;
                }
            }
        }

        public bool insertAirport(AdminAirport inAir)
        {
            using (var db = new WAPPContext())
            {
                try
                {
                    var newAir = new Airport();
                    newAir.Name = inAir.Name;
                    newAir.Country = inAir.Country;
                    db.Airport.Add(newAir);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    return false;
                }
            }
        }

        public bool editAirplane(int id, AdminAirplane inAir)
        {
            var db = new WAPPContext();
            try
            {
                var editAir = db.Airplanes.Find(id);
                editAir.Name = inAir.Name;
                editAir.Seats = inAir.Seats;
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public bool editAirport(int id, AdminAirport inAir)
        {
            var db = new WAPPContext();
            try
            {
                var editAir = db.Airport.Find(id);
                editAir.Name = inAir.Name;
                editAir.Country = inAir.Country;
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public AdminAirplane oneAirplane(int id)
        {
            var db = new WAPPContext();
            var e = db.Airplanes.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {

                var one = new AdminAirplane()
                {
                    ID = e.ID,
                    Name = e.Name,
                    Seats = e.Seats
                };

                return one;
            }
        }

        public AdminAirport oneAirport(int id)
        {
            var db = new WAPPContext();
            var e = db.Airport.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {

                var one = new AdminAirport()
                {
                    ID = e.ID,
                    Name = e.Name,
                    Country = e.Country
                };

                return one;
            }
        }

        public bool deleteAirplane(int id)
        {
            var db = new WAPPContext();
            try
            {
                db.Airplanes.Remove(db.Airplanes.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public bool deleteAirport(int id)
        {
            var db = new WAPPContext();
            try
            {
                db.Airport.Remove(db.Airport.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        public List<int> getInfo()
        {
            List<int> info = new List<int>();
            var db = new WAPPContext();
            info.Add(db.Employee.Count());
            info.Add(db.Customer.Count());
            info.Add(db.Airplanes.Count());
            info.Add(db.Airport.Count());
            info.Add(db.Booking.Count());
            info.Add(db.Flight.Count());
            return info;
        }

        public List<AdminBooking> listBookings()
        {
            var db = new WAPPContext();
            var list = db.Booking.Select(s => new AdminBooking()
            {
                ContactName = s.Customers.FirstOrDefault().FirstName + " " + s.Customers.FirstOrDefault().LastName,
                cID = s.Customers.FirstOrDefault().ID,
                ID = s.ID,
                RTString = s.RoundTrip.ToString(),
                Travelers = s.Travelers,
                TravelDate = s.Flights.FirstOrDefault().TravelDate
            }).ToList();
            foreach (var i in list)
            {
                String t = i.RTString;
                i.RTString = roundtripToString(t);
            }
            return list;
        }

        private String roundtripToString(String b)
        {
            String r = "";
            if (b.Equals("True"))
            {
                r = "Tur/Retur";
            }
            else
            {
                r = "En vei";
            }
            return r;
        }

        public bool changeFlight(int oldflight, int newflight, int bookingID)
        {
            var db = new WAPPContext();
            try
            {
                var booking = db.Booking.Find(bookingID);
                var old = db.Flight.Find(oldflight);
                old.Seats += booking.Travelers;
                var nju = db.Flight.Find(newflight);
                nju.Seats -= booking.Travelers;

                var oldindex = booking.Flights.IndexOf(old);
                booking.Flights.Remove(old);
                booking.Flights.Insert(oldindex, nju);
                db.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                LogException(exception);
                return false;
            }
        }

        private static void LogException(Exception exception)
        {
            var logfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WAPPException.log");
          
          




            Debug.WriteLine(logfile);
            try
            {

                using (StreamWriter sw = new StreamWriter(logfile, true))

                {
                    sw.WriteLine("********** {0} **********", DateTime.Now);
                    sw.WriteLine("Exception: " + exception.Message);
                    sw.WriteLine("Stack Trace: " + exception.StackTrace);
                    sw.WriteLine("");
                    sw.WriteLine("");

                }

            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.Message);
            }
        }
    }
}
