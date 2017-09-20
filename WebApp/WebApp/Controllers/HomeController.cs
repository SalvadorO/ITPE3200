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
            return View();
        }
        [HttpPost]
        public ActionResult Flight(Booking inFlightBooking)
        {
            //var db = new DBWebApp();
            //var newFlightBooking = db.flightRegistration(inFlightBooking);
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
            //var db = new DBWebApp();
            //db.customerList(finalBooking.customers);
            TempData["finalBooking"] = finalBooking;
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            var finalView = (FinalBooking)TempData["finalBooking"];
            TempData["toDataBase"] = finalView;
            return View(finalView);
        }
        [HttpPost]
        public ActionResult Confirmation(FinalBooking toDataBase)
        {
            toDataBase = (FinalBooking)TempData["toDataBase"];
            //Legger inn i database
            return View();
        }
        
        public ActionResult FinishedBooking()
        {
            return View();
        }

    }
}