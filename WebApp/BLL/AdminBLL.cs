﻿using System;
using System.Collections.Generic;
using WebApp.DAL;
using WebApp.Model;

namespace WebApp.BLL
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
        public bool usernameExist(String uname)
        {
            return new AdminDAL().usernameExist(uname);
        }
        public String getUsername(int id)
        {
            return new AdminDAL().getUsername(id);
        }
        public bool correctOldPassword(int id, String op)
        {
            return new AdminDAL().correctOldPassword(id,op);
        }
        public bool editEmployeeLogin(int id, EmployeeEditLogin inEEL)
        {
            return new AdminDAL().editEmployeeLogin(id,inEEL);
        }
        public List<AdminFlight> listAllFlights()
        {
            return new AdminDAL().listAllFlights();
        }
    }
}