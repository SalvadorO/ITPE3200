using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.DAL
{
    public interface IAdminRepository
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

        List<AdminViewFlight> listAllFlights();
        bool insertFlight(AdminFlight inFlight);
        bool deleteFlight(int id);
        AdminFlight oneFlight(int id);
        List<AdminAirplane> listAirplanes();
        List<AdminAirport> listAirports();
        bool editFlight(int id, AdminFlight ef);

        List<int> getInfo();
        List<AdminCustomer> getPassengers(int id);

        bool insertAirport(AdminAirport inAir);
        bool deleteAirport(int id);
        bool editAirport(int id, AdminAirport inAir);
        AdminAirport oneAirport(int id);

        bool insertAirplane(AdminAirplane inAir);
        bool deleteAirplane(int id);
        bool editAirplane(int id, AdminAirplane inAir);
        AdminAirplane oneAirplane(int id);

        AdminCustomer searchCustomer(int id);
        List<AdminViewFlight> searchFlight(SearchFlight search);
        Employee searchEmployee(String uname);
        AdminBooking searchBooking(int id);

        List<AdminViewFlight> customerBooking(int id);
        List<AdminBooking> listBookings();
        bool changeFlight(int oldflight, int newflight, int bookingID);

        String getUsername(int id);
        bool correctOldPassword(int id, String op);
        bool editEmployeeLogin(int id, EmployeeEditLogin inEEL);
        bool getShadow(EmployeeLogin inEmp);



    }
}
