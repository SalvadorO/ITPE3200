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
            return  _repository.getShadow(inEmp);
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
            return _repository.getUsername(id);
        }
        public bool correctOldPassword(int id, String op)
        {
            return _repository.correctOldPassword(id,op);
        }
        public bool editEmployeeLogin(int id, EmployeeEditLogin inEEL)
        {
            return _repository.editEmployeeLogin(id,inEEL);
        }
        public List<AdminViewFlight> listAllFlights()
        {
            return _repository.listAllFlights();
        }
        public AdminFlight oneFlight(int id)
        {
            return _repository.oneFlight(id);
        }
        public List<AdminViewFlight> searchFlight(SearchFlight search)
        {
            return _repository.searchFlight(search);
        }
        public List<AdminCustomer> searchCustomer(int id)
        {
            
            var list = new List<AdminCustomer>();
            var item = _repository.searchCustomer(id);
            if (item == null) return null;
            list.Add(item);
            return list;
        }

        public List<Employee> searchEmployee(String uname)
        {
            var list = new List<Employee>();
            var item = _repository.searchEmployee(uname);
            if (item == null) return null;
            list.Add(item);
            return list;
        }

        public List<AdminBooking> searchBooking(int id)
        {
            var list = new List<AdminBooking>();
            var item = _repository.searchBooking(id);
            if (item == null) return null;
            list.Add(item);
            return list;
        }
        public List<AdminAirplane> listAirplanes()
        {
            return _repository.listAirplanes();
        }
        public bool insertFlight(AdminFlight inFlight)
        {
            return _repository.insertFlight(inFlight);
        }
        public bool editFlight(int id, AdminFlight ef)
        {
            return _repository.editFlight(id, ef);
        }
        public bool deleteFlight(int id)
        {
            return _repository.deleteFlight(id);
        }
        public bool insertAirplane(AdminAirplane inAir)
        {
            return _repository.insertAirplane(inAir);
        }
        public bool editAirplane(int id, AdminAirplane inAir)
        {
            return _repository.editAirplane(id, inAir);
        }
        public List<AdminAirport> listAirports()
        {
            return _repository.listAirports();
        }
        public bool insertAirport(AdminAirport inAir)
        {
            return _repository.insertAirport(inAir);
        }
        public bool editAirPlane(int id, AdminAirplane inAir)
        {
            return _repository.editAirplane(id, inAir);
        }
        public bool editAirport(int id, AdminAirport inAir)
        {
            return _repository.editAirport(id, inAir);
        }
        public AdminAirplane oneAirplane(int id)
        {
            return _repository.oneAirplane(id);
        }
        public AdminAirport oneAirport(int id)
        {
            return _repository.oneAirport(id);
        }
        public bool deleteAirplane(int id)
        {
            return _repository.deleteAirplane(id);
        }
        public bool deleteAirport(int id)
        {
            return _repository.deleteAirport(id);
        }

        public List<int> getInfo()
        {
            return _repository.getInfo();
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
            return _repository.customerBooking(id);
        }

        public List<AdminCustomer> detailCustomer(int id)
        {
            return _repository.detailCustomer(id);
        }

        public List<AdminCustomer> getPassengers(int id)
        {
            return _repository.getPassengers(id);
        }

        public List<AdminBooking> listBookings()
        {
            return _repository.listBookings();
        }

        public bool changeFlight(int oldflight, int newflight, int bookingID)
        {
            return _repository.changeFlight(oldflight,newflight,bookingID);
        }
    }
}
