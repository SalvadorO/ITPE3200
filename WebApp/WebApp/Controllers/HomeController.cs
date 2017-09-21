using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Flight()
        {
            new WebAppContext();
            return View();
        }
        [HttpPost]
        public ActionResult Flight(Booking inFlightBooking)
        {
            TempData["flightBooking"] = inFlightBooking;
            return RedirectToAction("Registration");
        }

        public ActionResult Registration()
        {
            FinalBooking finalBooking = new FinalBooking();
            finalBooking.booking = (Booking)TempData["flightBooking"];
            TempData["flightBooking"] = finalBooking.booking;
            return View(finalBooking);
        }
        [HttpPost]
        public ActionResult Registration(FinalBooking finalBooking)
        {
            finalBooking.booking = (Booking)TempData["flightBooking"];
            TempData["finalBooking"] = finalBooking;
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            var finalView = (FinalBooking)TempData["finalBooking"];
            TempData["toDataBase"] = finalView;
            return View(finalView);
        }
 
        public ActionResult pushDatabase()
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                bool pushOK = db.pushToDataBase((FinalBooking)TempData["toDataBase"]);
                if (pushOK)
                {
                    return RedirectToAction("FinishedBooking");
                }
            }
            return View();
        }
        
        public ActionResult FinishedBooking()
        {
            return View();
        }

    }
}