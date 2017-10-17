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
        public List<AdminViewFlight> listAllFlights()
        {
            return new AdminDAL().listAllFlights();
        }
        public AdminFlight oneFlight(int id)
        {
            return new AdminDAL().oneFlight(id);
        }
        public List<AdminAirplane> listAirplanes()
        {
            return new AdminDAL().listAirplanes();
        }
        public bool insertFlight(AdminFlight inFlight)
        {
            return new AdminDAL().insertFlight(inFlight);
        }
        public bool editFlight(int id, AdminFlight ef)
        {
            return new AdminDAL().editFlight(id, ef);
        }
        public bool insertAirplane(AdminAirplane inAir)
        {
            return new AdminDAL().insertAirplane(inAir);
        }
        public bool editAirplane(int id, AdminAirplane inAir)
        {
            return new AdminDAL().editAirplane(id, inAir);
        }
        public List<AdminAirport> listAirports()
        {
            return new AdminDAL().listAirports();
        }
        public bool insertAirport(AdminAirport inAir)
        {
            return new AdminDAL().insertAirport(inAir);
        }
        public bool editAirPlane(int id, AdminAirplane inAir)
        {
            return new AdminDAL().editAirplane(id, inAir);
        }
        public bool editAirPort(int id, AdminAirport inAir)
        {
            return new AdminDAL().editAirport(id, inAir);
        }
        public AdminAirplane oneAirplane(int id)
        {
            return new AdminDAL().oneAirplane(id);
        }
        public AdminAirport oneAirport(int id)
        {
            return new AdminDAL().oneAirport(id);
        }
        public bool deleteAirplane(int id)
        {
            return new AdminDAL().deleteAirplane(id);
        }
        public bool deleteAirport(int id)
        {
            return new AdminDAL().deleteAirport(id);
        }

        public List<int> getInfo()
        {
            return new AdminDAL().getInfo();
        }
    }
}
