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
           /* List<Airports> air = new List<Airports>()
            {
                new Airports{Name = "Oslo"},
                new Airports{Name = "Barcelona"},
                new Airports{Name = "Las Vegas"},
                new Airports{Name = "Paris"},
                new Airports{Name = "Bardufoss"},
                new Airports{Name = "Svalbard"},
            };

            List<Flights> flight = new List<Flights>()
            {
                new Flights{DepartureTime = "00:00", Departure = 1, Destination = 2, DestinationTime = "04:00", TravelDate = "10/10/10", ClassType = "Luxus" },
                new Flights{DepartureTime = "00:00", Departure = 2, Destination = 1, DestinationTime = "04:00", TravelDate = "14/10/10", ClassType = "Luxus" },
                new Flights{DepartureTime = "00:00", Departure = 4, Destination = 5, DestinationTime = "04:00", TravelDate = "15/10/10", ClassType = "Luxus" },
                new Flights{DepartureTime = "00:00", Departure = 5, Destination = 4, DestinationTime = "04:00", TravelDate = "18/10/10", ClassType = "Luxus" }
            };
            var db =  new WebAppContext();
            foreach(var item in air){
                db.Airports.Add(item);
            }
            foreach(var item in flight)
            {
                db.Flights.Add(item);
            }
            db.SaveChanges();*/

            return View();
        }

        //Finner og lister treff etter søk presentert i partial view.
        [HttpPost]
        public ActionResult ChooseFlight(SearchBooking searchFlight)
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                TempData["booking"] = searchFlight.booking;
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
            var db = new WebAppContext();
            var FoundAirport = (from f in db.Airports where f.Name.StartsWith(Prefix) select new { f.Name });
            return Json(FoundAirport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registration(int id)
        {
            var db = new DBWebApp();
            FinalBooking finalBooking = new FinalBooking();
            finalBooking.flight = db.getFlight(id);
            finalBooking.booking = (Booking)TempData["booking"];
            finalBooking.booking.flightId = id;
            TempData["newbooking"] = finalBooking.booking;
            return View(finalBooking);
        }
        [HttpPost]
        public ActionResult Registration(FinalBooking finalBooking)
        {
                finalBooking.booking = (Booking)TempData["newbooking"];
                TempData["finalBooking"] = finalBooking;
                return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            var finalView = (FinalBooking)TempData["finalBooking"];
            var db = new DBWebApp();
            finalView.flight = db.getFlight(finalView.booking.flightId);
            TempData["toDataBase"] = finalView;
            //System.Diagnostics.Debug.WriteLine("DEP1** " + finalView.flight.departure);
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