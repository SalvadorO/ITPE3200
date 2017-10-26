using System;
using System.Web.Mvc;
using WebApp.BLL;
using WebApp.Model;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {

        private IAdminBLL _AdminBll;

        public AdminController()
        {
            _AdminBll = new AdminBLL();
        }

        public AdminController(IAdminBLL stub)
        {
            _AdminBll = stub;
        }

        public ActionResult LogIn()
        {
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
            }
            if ((bool)Session["LoggedIn"] == true)
            {
                return RedirectToAction("MainPage");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(EmployeeLogin loggedIn)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.EmpExists(loggedIn))
                {
                        Session["LoggedIn"] = true;
                        ViewBag.LoggedIn = true;
                        Session["Username"] = loggedIn.Username;
                    return RedirectToAction("MainPage");
                }
                else
                {
                    ViewBag.LoggedIn = false;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult MainPage()
        {
                if((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View(_AdminBll.getInfo());
        }

        public ActionResult Register()
        {
            /*
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            */
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(EmployeeRegister inEmp)
        {
            if (ModelState.IsValid)
            {
                var bll = _AdminBll;
                if (bll.usernameExist(inEmp.Username))
                {
                    ViewBag.UsernameExist = true;
                    return View();
                }
                if(bll.insertEmployee(inEmp))
                {
                    TempData["NewEmployee"] = true;
                    return RedirectToAction("ListEmployee");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult ListEmployee()
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            if (TempData["NewEmployee"] != null)
            {
                ViewBag.NewEmployee = (bool)TempData["NewEmployee"];
            }
            return View();
        }

        public ActionResult ListAllEmployees()
        {
            return PartialView("_ListEmployee", _AdminBll.listEmployee());
        }

        public ActionResult SearchEmployee(String uname)
        {
            var f = _AdminBll.searchEmployee(uname);
            if (f == null) return null;
            return PartialView("_ListEmployee", f);
        }
        public ActionResult SearchBooking(int id)
        {
            var f = _AdminBll.searchBooking(id);
            if (f == null) return null;
            return PartialView("ListBooking", f);
        }
        public ActionResult DetailEmployee(int id)
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View(_AdminBll.oneEmployee(id));
        }

        public ActionResult EditEmployee(int id)
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            if (TempData["SelfDelete"] != null)
            {
                ViewBag.SelfDelete = (bool)TempData["SelfDelete"];
            }
            return View(_AdminBll.oneEmployee(id));
        }

        [HttpPost]
        public ActionResult EditEmployee(int id,Employee inEmp)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.editEmployee(id, inEmp))
                {
                    return RedirectToAction("ListEmployee");
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            var bll = _AdminBll;

                if (bll.getUsernameID((String)Session["Username"]) == id)
                {
                    TempData["SelfDelete"] = true;
                    return RedirectToAction("EditEmployee", new { id = id });
                }
            if(bll.deleteEmployee(id))
            {
                return RedirectToAction("ListEmployee");
            }
            return RedirectToAction("Error");
        }

        public ActionResult EditLogIn(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            var EEL = new EmployeeEditLogin();
            EEL.Username = _AdminBll.getUsername(id);
            return View(EEL);
        }
        [HttpPost]
        public ActionResult EditLogIn(int id, EmployeeEditLogin inEEL)
        {
            if (ModelState.IsValid)
            {
                var bll = _AdminBll;
                if (bll.correctOldPassword(id, inEEL.OldPassword))
                {
                    if (bll.editEmployeeLogin(id, inEEL))
                    {
                        ViewBag.EditLogin = true;
                        return View();
                    }
                    else
                    {
                        ViewBag.EditLogin = false;
                        return View();
                    }
                }
                else
                {
                    ViewBag.OPOK = false;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ListCustomer()
        {
           if ((bool)Session["LoggedIn"] == false)
           {
             return RedirectToAction("LogIn");
           }
            return View();
        }

        public ActionResult ListContactPersons()
        {
            return PartialView("_ListCustomer", _AdminBll.listContactPersons());
        }

        public ActionResult EditCustomer(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View(_AdminBll.oneCustomer(id));
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, AdminCustomer inCust)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.editCustomer(id, inCust))
                {
                    return RedirectToAction("ListCustomer");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult DeleteCustomer(int id)
        {
            if (_AdminBll.deleteCustomer(id))
            {
                return RedirectToAction("ListCustomer");
            }
            return RedirectToAction("Error");
        }


        public ActionResult CustomerBooking(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            if (TempData["ChangeFlight"] != null)
            {
                ViewBag.ChangeFlight = (bool)TempData["ChangeFlight"];
            }
            return View(_AdminBll.customerBooking(id));
        }

        public ActionResult DetailCustomer(int id)
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                   return RedirectToAction("LogIn");
                }
            return View(_AdminBll.detailCustomer(id));
        }

        public ActionResult ListBooking()
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View(_AdminBll.listBookings());
        }

        public ActionResult ListFlight()
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            if (TempData["FlightInsert"] != null)
            {
                ViewBag.FlightInsert = (bool)TempData["FlightInsert"];
            }
            if(TempData["FlightEdit"] != null)
            {
                ViewBag.FlightEdit = (bool)TempData["FlightEdit"];
            }
            return View();
        }
        
        public ActionResult ListAllFlights()
        {
            return PartialView("_ListFlight", _AdminBll.listAllFlights());
        }
        public ActionResult SearchFlights(SearchFlight search)
        {
            return PartialView("_ListFlight", _AdminBll.searchFlight(search));
        }

        public ActionResult SearchChangeFlights(SearchFlight search)
        {
            return PartialView("_ListChangeFlight", _AdminBll.searchFlight(search));
        }

        public void ChangeFlight(int oldflight, int newflight, int bookingID)
        {
             if(_AdminBll.changeFlight(oldflight, newflight, bookingID))
            {
                 TempData["ChangeFlight"]= true;
            }
            else
            {
                TempData["ChangeFlight"] = false;
            }
        }

        public ActionResult SearchCustomers(int id)
        {
            var f = _AdminBll.searchCustomer(id);
            if (f == null) return null;
            return PartialView("_ListCustomer", f);
        }

        public ActionResult NewFlight()
        {
                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            var f = new AdminFlight();
            var bll = _AdminBll;
            f.Airports = bll.listAirports();
            f.Airplanes = bll.listAirplanes();
            f.Airports.Insert(0,null);
            f.Airplanes.Insert(0,null);
            return View(f);
        }

        [HttpPost]
        public ActionResult NewFlight(AdminFlight inFlight)
        {
            if (ModelState.IsValid)
            {
                if(_AdminBll.insertFlight(inFlight))
                {
                    TempData["FlightInsert"] = true;
                    return RedirectToAction("ListFlight");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult EditFlight(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }

            var bll = _AdminBll;
            var f = bll.oneFlight(id);
            f.Airplanes = bll.listAirplanes();
            f.Airports = bll.listAirports();
            return View(f);
        }
        [HttpPost]
        public ActionResult EditFlight(int id, AdminFlight inFlight)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.editFlight(id, inFlight)) {
                    TempData["FlightEdit"] = true;
                    return RedirectToAction("ListFlight");
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult DeleteFlight(int id)
        {
            if (_AdminBll.deleteFlight(id))
            {
                return RedirectToAction("ListFlight");
            }
            return RedirectToAction("Error");
        }

        public ActionResult Passengers(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                   return RedirectToAction("LogIn");
                }

            return View(_AdminBll.getPassengers(id));
        }

        public ActionResult ListAirplane()
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View(_AdminBll.listAirplanes());
        }

        public ActionResult NewAirplane()
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }

            return View();
        }
        [HttpPost]
        public ActionResult NewAirplane(AdminAirplane inAir)
        {
            if (ModelState.IsValid)
            {
                if(_AdminBll.insertAirplane(inAir))
                {
                    return RedirectToAction("ListAirplane");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult EditAirplane(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }

            return View(_AdminBll.oneAirplane(id));
        }
        [HttpPost]
        public ActionResult EditAirplane(int id, AdminAirplane inAir)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.editAirplane(id, inAir))
                {
                    return RedirectToAction("ListAirplane");
                }
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult DeleteAirplane(int id)
        {
            if (_AdminBll.deleteAirplane(id))
            {
                return RedirectToAction("ListAirplane");
            }
            return RedirectToAction("Error");
        }

        public ActionResult ListAirport()
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                   return RedirectToAction("LogIn");
                }

            return View(_AdminBll.listAirports());
        }

        public ActionResult NewAirport()
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }
            return View();
        }
        [HttpPost]
        public ActionResult NewAirport(AdminAirport inAir)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.insertAirport(inAir))
                {
                    return RedirectToAction("ListAirport");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult EditAirport(int id)
        {

                if ((bool)Session["LoggedIn"] == false)
                {
                    return RedirectToAction("LogIn");
                }

            return View(_AdminBll.oneAirport(id));
        }
        [HttpPost]
        public ActionResult EditAirport(int id, AdminAirport inAir)
        {
            if (ModelState.IsValid)
            {
                if (_AdminBll.editAirport(id,inAir))
                {
                    return RedirectToAction("ListAirport");
                }
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult DeleteAirport(int id)
        {
            if (_AdminBll.deleteAirport(id))
            {
                return RedirectToAction("ListAirport");
            }
            return RedirectToAction("Error");
        }

        public ActionResult LogOut()
        {
            Session["LoggedIn"] = false;
            Session["Username"] = null;
            return RedirectToAction("LogIn");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}