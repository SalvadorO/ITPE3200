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

        public List<AdminCustomer> detailCustomer(int id)
        {
            List<AdminCustomer> list = new List<AdminCustomer>();
            list.Add(oneCustomer(id));
            return list;
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

        public int getUsernameID(string uname)
        {
            if(uname == "")
            {
                return 1;
            }
            else
            {
                return 0;
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
