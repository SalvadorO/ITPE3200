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
        // View med søk
        public ActionResult ChooseFlight()
        {
            return View();
        }

        //Finner og lister treff etter søk presentert i partial view.
        [HttpPost]
        public ActionResult ChooseFlight(SearchBooking searchFlight)
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                SearchBooking search = new SearchBooking();
                search.flights = db.searchFlights(searchFlight);
                return View(search);
            }
            return View();
        }

        //Finner mulige flyplasser i Airport-databasen.
        [HttpPost]
        public JsonResult FindAirport(String Prefix)
        {
            System.Diagnostics.Debug.WriteLine("HALLLLLLOOOOOOO");
            var db = new WebAppContext();
            var FoundAirport = (from f in db.Airports where f.Name.StartsWith(Prefix) select new { f.Name });
            return Json(FoundAirport, JsonRequestBehavior.AllowGet);
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