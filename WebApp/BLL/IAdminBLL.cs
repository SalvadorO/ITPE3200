using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.BLL
{
    public interface IAdminBLL
    {
        Employee oneEmployee(int id);
        List<Employee> listEmployee();
        bool insertEmployee(EmployeeRegister inEmp);
        bool usernameExist(String uname);
        int getUsernameID(String uname);
        bool editEmployee(int id, Employee inEmp);
        bool deleteEmployee(int id);

        AdminCustomer oneCustomer(int id);
        List<AdminCustomer> listContactPersons();
        List<AdminCustomer> detailCustomer(int id);
        bool editCustomer(int id, AdminCustomer inCust);
        bool deleteCustomer(int id);
    }
}
