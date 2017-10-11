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
    }
}
