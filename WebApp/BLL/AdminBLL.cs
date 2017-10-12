using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppAdmin.DAL;
using WebAppAdmin.Model;

namespace WebAppAdmin.BLL
{
    public class AdminBLL
    {

        public bool EmpExists(EmployeeLogin inEmp)
        {
            return new AdminDAL().getShadow(inEmp);
        }
        public bool insertEmp(EmployeeRegister inEmp)
        {
            return new AdminDAL().insertEmployee(inEmp);
        }
        public bool editEmployee(int id, Employee inEmp)
        {
            return new AdminDAL().editEmployee(id,inEmp);
        }

        public bool deleteEmployee(int id)
        {
            return new AdminDAL().deleteEmployee(id);
        }

        public List<Employee> listEmployee()
        {
            return new AdminDAL().listEmployee();
        }

        public Employee oneEmployee(int id)
        {
            return new AdminDAL().oneEmployee(id);
        }

        public int getUsernameID(String uname)
        {
            return new AdminDAL().getUsernameID(uname);
        }
    }
}
