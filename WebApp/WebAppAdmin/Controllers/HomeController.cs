using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAdmin.BLL;
using WebAppAdmin.Model;

namespace WebAppAdmin.Controllers
{
    public class HomeController : Controller
    {
        // Kode brukt fra forelesning
        public ActionResult LogIn()
        {
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
            }
            else
            {
                ViewBag.LoggedIn = (bool)Session["LoggedIn"];
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
                return View();
            }
            else
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(EmployeeRegister inEmp)
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("****MODELSTATE*****");
                return RedirectToAction("Error");
            }
            if(new AdminBLL().insertEmp(inEmp))
            {
                return View();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("****FALSE*****");
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}