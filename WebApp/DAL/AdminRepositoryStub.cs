using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.DAL
{
    public class AdminRepositoryStub : IAdminRepository
    {
        public bool changeFlight(int oldflight, int newflight, int bookingID)
        {
            if(bookingID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool correctOldPassword(int id, string op)
        {
            
            if(id == 0)
            {
                return false;
            }
            if(op == "")
            {
                return false;
            }
            return true;
        }

        public List<AdminViewFlight> customerBooking(int id)
        {
            List<AdminViewFlight> list = new List<AdminViewFlight>();
            if (id == 0)
            {
                var f = new AdminViewFlight();
                f.ID = 0;
                list.Add(f);
                return list;
            }
            else
            {
                var f = new AdminViewFlight()
                {
                    ID = 1,
                    Departure = "Oslo",
                    DepartureTime = "00:00",
                    Destination = "Bardufoss",
                    DestinationTime = "00:00",
                    Airplane = "Boeing737",
                    ClassType = "Økonomi",
                    Price = 500,
                    Seats = 500,
                    TravelDate = "00/00/00"
                };
                list.Add(f);
                list.Add(f);
                list.Add(f);
                return list;
            }
        }

        public bool deleteAirplane(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool deleteAirport(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool deleteCustomer(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool deleteEmployee(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool deleteFlight(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<AdminCustomer> detailCustomer(int id)
        {
            List<AdminCustomer> list = new List<AdminCustomer>();
            list.Add(oneCustomer(id));
            return list;
        }

        public Employee detailEmployee(int id)
        {
            return oneEmployee(id);
        }

        public bool editAirplane(int id, AdminAirplane inAir)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool editAirport(int id, AdminAirport inAir)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool editCustomer(int id, AdminCustomer inCust)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool editEmployee(int id, Employee inEmp)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool editEmployeeLogin(int id, EmployeeEditLogin inEEL)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool editFlight(int id, AdminFlight ef)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<int> getInfo()
        {
            List<int> list = new List<int>();
            list.Add(1111);
            list.Add(2222);
            list.Add(3333);
            list.Add(4444);
            list.Add(5555);
            return list;
        }

        public List<AdminCustomer> getPassengers(int id)
        {
            List<AdminCustomer> list = new List<AdminCustomer>();
            if (id == 0)
            {
                var customer = new AdminCustomer();
                customer.ID = 0;
                list.Add(customer);
                return list;
            }
            else
            {
                var customer = new AdminCustomer()
                {
                    ID = 1,
                    FirstName = "Ola",
                    LastName = "Nordmann",
                    ContactPerson = false
                };
                list.Add(customer);
                list.Add(customer);
                list.Add(customer);
                return list;
            }
        }

        public bool getShadow(EmployeeLogin inEmp)
        {
            if(inEmp.Password != "PassordOla")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getUsername(int id)
        {
            if(id == 0)
            {
                return "";
            }
            else
            {
                return "Olanord";
            }
        }

        public int getUsernameID(string uname)
        {
            if(uname == "Olanord")
            {
                return 1;
            }
            else if(uname == "olaNy")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public bool insertAirplane(AdminAirplane inAir)
        {
            if (inAir.Name == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool insertAirport(AdminAirport inAir)
        {
            if (inAir.Name == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool insertEmployee(EmployeeRegister inEmp)
        {
            if (inEmp.FirstName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool insertFlight(AdminFlight inFlight)
        {
            if (inFlight.DepartureTime == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<AdminAirplane> listAirplanes()
        {
            var plane = new AdminAirplane()
            {
                ID = 1,
                Name = "Boeing737",
                Seats = 500
            };
            var list = new List<AdminAirplane>();
            list.Add(plane);
            return list;
        }

        public List<AdminAirport> listAirports()
        {
            var port = new AdminAirport()
            {
                ID = 1,
                Name = "Oslo",
                Country = "Norge"
            };
            var port2 = new AdminAirport()
            {
                ID = 2,
                Name = "Bardufoss",
                Country = "Norge"
            };
            var list = new List<AdminAirport>();
            list.Add(port);
            list.Add(port2);
            return list;
        }

        public List<AdminViewFlight> listAllFlights()
        {
            var list = new List<AdminViewFlight>();
            var flight = new AdminViewFlight()
            {
                ID = 1,
                Departure = "Oslo",
                DepartureTime = "00:00",
                Destination = "Bardufoss",
                DestinationTime = "00:00",
                Airplane = "Boeing737",
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            list.Add(flight);
            list.Add(flight);
            list.Add(flight);

            return list;
        }

        public List<AdminBooking> listBookings()
        {
            List<AdminBooking> list = new List<AdminBooking>();
            var booking = new AdminBooking()
            {
                ContactName = "Ola Nordmann",
                cID = 1,
                ID = 1,
                RTString = "Tur/Retur",
                Travelers = 1,
                TravelDate = "00/00/00"
            };
            list.Add(booking);
            list.Add(booking);
            list.Add(booking);
            return list;
        }

        public List<AdminCustomer> listContactPersons()
        {
            List<AdminCustomer> list = new List<AdminCustomer>();
            var contact = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                ContactPerson = true
            };
            list.Add(contact);
            list.Add(contact);
            list.Add(contact);
            return list;
        }

        public List<Employee> listEmployee()
        {
            List<Employee> list = new List<Employee>();
            var employee = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                Username = "Olanord"
            };
            list.Add(employee);
            list.Add(employee);
            list.Add(employee);
            return list;
        }

        public AdminAirplane oneAirplane(int id)
        {
            if (id == 0)
            {
                var airplane = new AdminAirplane();
                airplane.ID = 0;
                return airplane;
            }
            else
            {
                var airplane = new AdminAirplane()
                {
                    ID = 1,
                    Name = "Boeing737",
                    Seats = 500
                };
                return airplane;
            }
        }

        public AdminAirport oneAirport(int id)
        {
            if (id == 0)
            {
                var airport = new AdminAirport();
                airport.ID = 0;
                return airport;
            }
            else
            {
                var airport = new AdminAirport()
                {
                    ID = 1,
                    Name = "Oslo",
                    Country = "Norge"
                };
                return airport;
            }
        }

        public AdminCustomer oneCustomer(int id)
        {
            if (id == 0)
            {
                var customer = new AdminCustomer();
                customer.ID = 0;
                return customer;
            }
            else
            {
                var customer = new AdminCustomer()
                {
                    ID = 1,
                    FirstName = "Ola",
                    LastName = "Nordmann",
                    PhoneNumber = "12345678",
                    EMail = "Ola@mail.no",
                    Address = "Osloveien 4",
                    ZipCode = "1234",
                    City = "Oslo",
                    ContactPerson = false
                };
                return customer;
            }
        }

        public Employee oneEmployee(int id)
        {
            if (id == 0)
            {
                var emp = new Employee();
                emp.ID = 0;
                return emp;
            }
            else
            {
                var emp = new Employee()
                {
                    ID = 1,
                    FirstName ="Ola",
                    LastName = "Nordmann",
                    PhoneNumber = "12345678",
                    EMail = "Ola@mail.no",
                    Address = "Osloveien 4",
                    ZipCode = "1234",
                    City = "Oslo",
                    Username = "Olanord"
                };
                return emp;
            }
        }

        public AdminFlight oneFlight(int id)
        {
            if (id == 0)
            {
                var f = new AdminFlight();
                f.ID = 0;
                return f;
            }
            else
            {
                var f = new AdminFlight()
                {
                    ID = 1,
                    Departure = 1,
                    DepartureTime = "00:00",
                    Destination = 2,
                    DestinationTime = "00:00",
                    Airplane = 1,
                    ClassType = "Økonomi",
                    Price = 500,
                    Seats = 500,
                    TravelDate = "00/00/00"
                };
                return f;
            }
        }

        public AdminBooking searchBooking(int id)
        {
            if (id == 0)
            {
                var f = new AdminBooking();
                f.ID = 0;
                return f;
            }
            else
            {
                var booking = new AdminBooking()
                {
                    ContactName = "Ola Nordmann",
                    cID = 1,
                    ID = 1,
                    RTString = "Tur/Retur",
                    Travelers = 1,
                    TravelDate = "00/00/00"
                };
                return booking;
            }
        }

        public AdminCustomer searchCustomer(int id)
        {
            if (id == 0)
            {
                var f = new AdminCustomer();
                f.ID = 0;
                return f;
            }
            else
            {
                var customer = new AdminCustomer()
                {
                    ID = 1,
                    FirstName = "Ola",
                    LastName = "Nordmann",
                    PhoneNumber = "12345678",
                    EMail = "Ola@mail.no",
                    Address = "Osloveien 4",
                    ZipCode = "1234",
                    City = "Oslo",
                    ContactPerson = false
                };
                return customer;
            }
        }

        public Employee searchEmployee(string uname)
        {
            if (uname == "")
            {
                var f = new Employee();
                f.ID = 0;
                return f;
            }
            else
            {
                var customer = new Employee()
                {
                    ID = 1,
                    FirstName = "Ola",
                    LastName = "Nordmann",
                    PhoneNumber = "12345678",
                    EMail = "Ola@mail.no",
                    Address = "Osloveien 4",
                    ZipCode = "1234",
                    City = "Oslo",
                    Username = "Olanord"
                };
                return customer;
            }
        }

        public List<AdminViewFlight> searchFlight(SearchFlight search)
        {
            List<AdminViewFlight> list = new List<AdminViewFlight>();
            if(search.Date == "")
            {
                var f = new AdminViewFlight();
                f.ID = 0;
                list.Add(f);
                return list;
            }
            else
            {
                var f = new AdminViewFlight()
                {
                    ID = 1,
                    Departure = "Oslo",
                    DepartureTime = "00:00",
                    Destination = "Bardufoss",
                    DestinationTime = "00:00",
                    Airplane = "Boeing737",
                    ClassType = "Økonomi",
                    Price = 500,
                    Seats = 500,
                    TravelDate = "00/00/00"
                };
                list.Add(f);
                list.Add(f);
                list.Add(f);
                return list;
            }
        }

        public bool usernameExist(string uname)
        {
            if(uname == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
