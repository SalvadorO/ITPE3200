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
            
            var db =  new WebAppContext();
            db.Database.Delete();

           var Airport1 = new Airport { Name = "Oslo", Country = "Norge" };
           var Airport2 = new Airport { Name = "Værnes", Country = "Norge" };
           var Airport3 = new Airport { Name = "Tromsø", Country = "Norge" };
           var Airport4 = new Airport { Name = "Bardufoss", Country = "Norge" };
           var Airport5 = new Airport { Name = "Torp", Country = "Norge" };
           var Airport6 = new Airport { Name = "Sola", Country = "Norge" };
           var Airport7 = new Airport { Name = "Flesland", Country = "Norge" };
           var Airport8 = new Airport { Name = "Hammerfest", Country = "Norge" };

           db.Airport.Add(Airport1);
           db.Airport.Add(Airport2);
           db.Airport.Add(Airport3);
           db.Airport.Add(Airport4);
           db.Airport.Add(Airport5);
           db.Airport.Add(Airport6);
           db.Airport.Add(Airport7);
           db.Airport.Add(Airport8);

           var Flight1 = new Flight { Departure = 1, DepartureTime = "10:00", Destination = 3, DestinationTime = "12:00", TravelDate = "02/10/2017", ClassType = "Økonomi",Seats = 50 };
           var Flight2 = new Flight { Departure = 3, DepartureTime = "12:00", Destination = 8, DestinationTime = "14:00", TravelDate = "02/10/2017", ClassType = "Økonomi", Seats = 50 };
           var Flight3 = new Flight { Departure = 8, DepartureTime = "15:00", Destination = 3, DestinationTime = "17:00", TravelDate = "02/10/2017", ClassType = "Økonomi", Seats = 50 };
           var Flight4 = new Flight { Departure = 3, DepartureTime = "19:00", Destination = 1, DestinationTime = "21:00", TravelDate = "02/10/2017", ClassType = "Økonomi", Seats = 50 };
           var Flight5 = new Flight { Departure = 3, DepartureTime = "13:00", Destination = 8, DestinationTime = "15:00", TravelDate = "02/10/2017", ClassType = "Økonomi", Seats = 50 };
           var Flight6 = new Flight { Departure = 8, DepartureTime = "10:00", Destination = 3, DestinationTime = "12:00", TravelDate = "02/10/2017", ClassType = "Økonomi", Seats = 50 };
          /* var Flight7 = new Flight { Departure = 3, DepartureTime = "14:00", Destination = 8, DestinationTime = "15:00", TravelDate = "30/09/2017", ClassType = "Økonomi" };
           var Flight8 = new Flight { Departure = 4, DepartureTime = "10:00", Destination = 1, DestinationTime = "11:35", TravelDate = "2/10/2017", ClassType = "Økonomi" };
           var Flight9 = new Flight { Departure = 1, DepartureTime = "10:00", Destination = 7, DestinationTime = "11:35", TravelDate = "30/09/2017", ClassType = "Økonomi" };
           var Flight10 = new Flight { Departure = 1, DepartureTime = "13:00", Destination = 2, DestinationTime = "14:00", TravelDate = "02/10/2017", ClassType = "Økonomi" };
            var Flight11 = new Flight { Departure = 1, DepartureTime = "18:00", Destination = 4, DestinationTime = "19:35", TravelDate = "30/09/2017", ClassType = "Økonomi" };
            var Flight12 = new Flight { Departure = 8, DepartureTime = "10:00", Destination = 3, DestinationTime = "12:00", TravelDate = "30/09/2017", ClassType = "Økonomi" };
            var Flight13 = new Flight { Departure = 3, DepartureTime = "14:00", Destination = 1, DestinationTime = "15:00", TravelDate = "30/09/2017", ClassType = "Økonomi" }; */

           db.Flight.Add(Flight1);
           db.Flight.Add(Flight2);
           db.Flight.Add(Flight3);
           db.Flight.Add(Flight4);
           db.Flight.Add(Flight5);
           db.Flight.Add(Flight6);
          /* db.Flight.Add(Flight7);
           db.Flight.Add(Flight8);
           db.Flight.Add(Flight9);
           db.Flight.Add(Flight10);
            db.Flight.Add(Flight11);
            db.Flight.Add(Flight12);
            db.Flight.Add(Flight13);*/


            db.SaveChanges();
            

            return View();
        }

        //Finner og lister treff etter søk presentert i partial view.
        [HttpPost]
        public ActionResult ChooseFlight(ViewModel searchFlight)
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                TempData["booking"] = searchFlight.booking;
                ViewModel search = new ViewModel();
                search.flight = new ViewFlight();
                search.travelflights = db.searchTravelFlights(searchFlight);
                System.Diagnostics.Debug.WriteLine("TRAVEL: " + search.travelflights.Count);
                TempData["tids"] = search.flight.travelIDs = db.filterIDs(search.travelflights);
                if (searchFlight.booking.roundTrip)
                {
                    search.returnflights = db.searchReturnFlight(searchFlight);
                    System.Diagnostics.Debug.WriteLine("REturn: " + search.returnflights.Count);
                    TempData["rids"] = search.flight.returnIDs = db.filterIDs(search.returnflights);
                }
                return PartialView("FlightPartial",search);
            }
            return View();
        }

        //Finner mulige flyplasser i Airport-databasen.
        [HttpPost]
        public JsonResult FindAirport(String Prefix)
        {
            var db = new WebAppContext();
            var FoundAirport = (from f in db.Airport where f.Name.StartsWith(Prefix) select new { f.Name });
            return Json(FoundAirport, JsonRequestBehavior.AllowGet);
        }

        public void Helper(int travelID, int returnID)
        {
            var db = new DBWebApp();
            ViewModel reg = new ViewModel();
            reg.flight = new ViewFlight();
            reg.booking = (ViewBooking)TempData["booking"];
            reg.flight.travelIDs = (List<List<int>>)TempData["tids"];
            reg.booking.chosenTravel = db.getFlights(reg.flight.travelIDs[travelID]);
            if (returnID != -1)
            {
                reg.flight.returnIDs = (List<List<int>>)TempData["rids"];
                reg.booking.chosenReturn = db.getFlights(reg.flight.returnIDs.ElementAt(returnID));
            }
            TempData["help"] = reg;
        }

        public ActionResult Registration()
        {
            var finalReg = (ViewModel)TempData["help"];
            TempData["newbooking"] = finalReg.booking;
            return View(finalReg);
        }
        [HttpPost]
        public ActionResult Registration(ViewModel finalBooking)
        {
            finalBooking.booking = (ViewBooking)TempData["newbooking"];
            TempData["reg"] = finalBooking;
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            var finalView = (ViewModel)TempData["reg"];
            TempData["toDataBase"] = finalView;
            return View(finalView);
        }
 
        public ActionResult pushDatabase()
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                bool pushOK = db.pushToDataBase((ViewModel)TempData["toDataBase"]);
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