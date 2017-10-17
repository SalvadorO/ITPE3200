using System;
using System.Web.Mvc;
using WebApp.BLL;
using WebApp.Model;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult LogIn()
        {
            if (Session["LoggedIn"] != null 
                && (bool)Session["LoggedIn"] == true)
            {
                return RedirectToAction("MainPage");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(EmployeeLogin loggedIn)
        {
            if (new AdminBLL().EmpExists(loggedIn))
            {
                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;
                Session["Username"] = loggedIn.Username;
                return RedirectToAction("MainPage");
            }
            else
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                return View();
            }
        }

        public ActionResult MainPage()
        {
            if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }
            return View(new AdminBLL().getInfo());
        }

        public ActionResult Register()
        {
            /*if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }*/
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(EmployeeRegister inEmp)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error");
            }
            var bll = new AdminBLL();
            if (bll.usernameExist(inEmp.Username))
            {
                ViewBag.UsernameExist = true;
                return View();
            }
            if(bll.insertEmp(inEmp))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ListEmployee()
        {
            if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }
            return View(new AdminBLL().listEmployee());
        }

        public ActionResult DetailEmployee(int id)
        {
            if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }
            return View(new AdminBLL().oneEmployee(id));
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
            return View(new AdminBLL().oneEmployee(id));
        }

        [HttpPost]
        public ActionResult EditEmployee(int id,Employee inEmp)
        {
            if (ModelState.IsValid)
            {
                if (new AdminBLL().editEmployee(id, inEmp))
                {
                    return RedirectToAction("ListEmployee");
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            var bll = new AdminBLL();
            if(bll.getUsernameID((String)Session["Username"]) == id)
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
            EEL.Username = new AdminBLL().getUsername(id);
            return View(EEL);
        }
        [HttpPost]
        public ActionResult EditLogIn(int id, EmployeeEditLogin inEEL)
        {
            var bll = new AdminBLL();
            if(bll.correctOldPassword(id, inEEL.OldPassword))
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
            return PartialView("_ListFlight", new AdminBLL().listAllFlights());
        }

        public ActionResult NewFlight()
        {
            if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }
            var f = new AdminFlight();
            var bll = new AdminBLL();
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
                if(new AdminBLL().insertFlight(inFlight))
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
            var bll = new AdminBLL();
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
                if (new AdminBLL().editFlight(id, inFlight)) {
                    TempData["FlightEdit"] = true;
                    return RedirectToAction("ListFlight");
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult DeleteFlight(int id)
        {
            if (new AdminBLL().deleteFlight(id))
            {
                return RedirectToAction("ListFlight");
            }
            return RedirectToAction("Error");
        }

        public ActionResult ListAirplane()
        {
            if ((bool)Session["LoggedIn"] == false)
            {
                return RedirectToAction("LogIn");
            }
            return View(new AdminBLL().listAirplanes());
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
                if(new AdminBLL().insertAirplane(inAir))
                {
                    return RedirectToAction("ListAirPlane");
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
            return View(new AdminBLL().oneAirplane(id));
        }
        [HttpPost]
        public ActionResult EditAirplane(int id, AdminAirplane inAir)
        {
            if (ModelState.IsValid)
            {
                if (new AdminBLL().editAirplane(id, inAir))
                {
                    return RedirectToAction("ListAirplane");
                }
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult DeleteAirplane(int id)
        {
            if(new AdminBLL().deleteAirplane(id))
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
            return View(new AdminBLL().listAirports());
        }

        public ActionResult NewAirport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAirport(AdminAirport inAir)
        {
            if (ModelState.IsValid)
            {
                if (new AdminBLL().insertAirport(inAir))
                {
                    return RedirectToAction("ListAirPort");
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
            return View(new AdminBLL().oneAirport(id));
        }
        [HttpPost]
        public ActionResult EditAirport(int id, AdminAirport inAir)
        {
            if (ModelState.IsValid)
            {
                if (new AdminBLL().editAirPort(id,inAir))
                {
                    return RedirectToAction("ListAirport");
                }
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult DeleteAirport(int id)
        {
            if (new AdminBLL().deleteAirport(id))
            {
                return RedirectToAction("ListAirport");
            }
            return RedirectToAction("Error");
        }

        public ActionResult LogOut()
        {
            Session["LoggedIn"] = null;
            Session["Username"] = null;
            return RedirectToAction("LogIn");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}