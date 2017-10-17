using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.BLL;
using WebApp.Model;

namespace Controllers
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

        // Kode brukt fra forelesning
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
            return View();
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
            return View();
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
            return View();
        }
        
        public ActionResult ListAllFlights()
        {
            return PartialView("_ListFlight", new AdminBLL().listAllFlights());
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