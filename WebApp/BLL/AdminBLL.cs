using System;
using System.Collections.Generic;
using WebApp.DAL;
using WebApp.Model;

namespace WebApp.BLL
{
    public class AdminBLL : IAdminBLL
    {
        private IAdminRepository _repository;

        public AdminBLL()
        {
            _repository = new AdminDAL();
        }

        public AdminBLL(IAdminRepository stub)
        {
            _repository = stub;
        }

        public bool EmpExists(EmployeeLogin inEmp)
        {
            return  new AdminDAL().getShadow(inEmp);
        }
        public bool insertEmployee(EmployeeRegister inEmp)
        {
            return _repository.insertEmployee(inEmp);
        }
        public bool editEmployee(int id, Employee inEmp)
        {
            return _repository.editEmployee(id,inEmp);
        }
        public bool deleteEmployee(int id)
        {
            return _repository.deleteEmployee(id);
        }
        public List<Employee> listEmployee()
        {
            return _repository.listEmployee();
        }
        public Employee oneEmployee(int id)
        {
            return _repository.oneEmployee(id);
        }
        public int getUsernameID(String uname)
        {
            return _repository.getUsernameID(uname);
        }
        public bool usernameExist(String uname)
        {
            return _repository.usernameExist(uname);
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
        public List<AdminViewFlight> searchFlight(SearchFlight search)
        {
            return new AdminDAL().searchFlight(search);
        }
        public List<AdminCustomer> searchCustomer(int id)
        {
            
            var list = new List<AdminCustomer>();
            var item = new AdminDAL().searchCustomer(id);
            if (item == null) return null;
            list.Add(item);
            return list;
        }

        public List<Employee> searchEmployee(String uname)
        {
            var list = new List<Employee>();
            var item = new AdminDAL().searchEmployee(uname);
            if (item == null) return null;
            list.Add(item);
            return list;
        }

        public List<AdminBooking> searchBooking(int id)
        {
            var list = new List<AdminBooking>();
            var item = new AdminDAL().searchBooking(id);
            if (item == null) return null;
            list.Add(item);
            return list;
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
        public bool deleteFlight(int id)
        {
            return new AdminDAL().deleteFlight(id);
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

        public List<AdminCustomer> listContactPersons()
        {
            return _repository.listContactPersons();
        }

        public AdminCustomer oneCustomer(int id)
        {
            return _repository.oneCustomer(id);
        }

        public bool editCustomer(int id, AdminCustomer inCust)
        {
            return _repository.editCustomer(id,inCust);
        }

        public bool deleteCustomer(int id)
        {
            return _repository.deleteCustomer(id);
        }
        public List<AdminViewFlight> customerBooking(int id)
        {
            return new AdminDAL().customerBooking(id);
        }

        public List<AdminCustomer> detailCustomer(int id)
        {
            return _repository.detailCustomer(id);
        }

        public List<AdminCustomer> getPassengers(int id)
        {
            return new AdminDAL().getPassengers(id);
        }

        public List<AdminBooking> listBookings()
        {
            return new AdminDAL().listBookings();
        }

        public bool changeFlight(int oldflight, int newflight, int bookingID)
        {
            return new AdminDAL().changeFlight(oldflight,newflight,bookingID);
        }
    }
}
