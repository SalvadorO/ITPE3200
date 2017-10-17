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
        public ActionResult ChooseFlight(ViewModel searchFlight)
        {
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                TempData["booking"] = searchFlight.booking;
                ViewModel search = new ViewModel();
                search.flight = new ViewFlight();
                search.travelflights = db.searchTravelFlights(searchFlight);
                TempData["tids"] = db.filterIDs(search.travelflights);
                if (searchFlight.booking.roundTrip)
                {
                    search.returnflights = db.searchReturnFlight(searchFlight);
                    TempData["rids"] = db.filterIDs(search.returnflights);
                }
                return PartialView("FlightPartial",search);
            }
            return RedirectToAction("Error");
        }

        //Oppdaterer pris i partial view dynamisk
        public int UpdateDynamicPrice(int trav, int ret, int num)
        {
            int price = 0;
            var db = new DBWebApp();
            price = db.getPrice(db.getFlights(((List<List<int>>)TempData["tids"])[trav])) * num;
            TempData.Keep("tids");
            if(ret != -1)
            {
                price += db.getPrice(db.getFlights(((List<List<int>>)TempData["rids"])[ret])) * num;
                TempData.Keep("rids");
            }
            return price;
        }

        //Finner mulige flyplasser i Airport-databasen.
        [HttpPost]
        public JsonResult FindAirport(String Prefix)
        {
            var db = new WebAppContext();
            var FoundAirport = (from f in db.Airport where f.Name.StartsWith(Prefix) select new { f.Name });
            return Json(FoundAirport, JsonRequestBehavior.AllowGet);
        }

        //Hjelper til med å sette opp registrerings-viewet
        public void Helper(int travelID, int returnID)
        {
            var db = new DBWebApp();
            ViewModel reg = new ViewModel();
            reg.flight = new ViewFlight();
            reg.booking = (ViewBooking)TempData["booking"];
            reg.flight.travelIDs = (List<List<int>>)TempData["tids"];
            reg.booking.chosenTravel = db.getFlights(reg.flight.travelIDs[travelID]);
            reg.booking.totalPrice = db.getPrice(reg.booking.chosenTravel) * reg.booking.travelers;
            if (returnID != -1)
            {
                reg.flight.returnIDs = (List<List<int>>)TempData["rids"];
                reg.booking.chosenReturn = db.getFlights(reg.flight.returnIDs.ElementAt(returnID));
                reg.booking.totalPrice += db.getPrice(reg.booking.chosenReturn) * reg.booking.travelers;
            }
            TempData["help"] = reg;
        }

        //View med registrering
        public ActionResult Registration()
        {
            var finalReg = (ViewModel)TempData["help"];
            TempData["newbooking"] = finalReg.booking;
            TempData.Keep("help");
            return View(finalReg);
        }

        //Sender informasjonen videre til bekreftelses-view
        [HttpPost]
        public ActionResult Registration(ViewModel finalBooking)
        {
            finalBooking.booking = (ViewBooking)TempData["newbooking"];
            TempData["reg"] = finalBooking;
            return RedirectToAction("Confirmation");
        }

        //View med all informasjon og modal til betalings-informasjon
        public ActionResult Confirmation()
        {
            var finalView = (ViewModel)TempData["reg"];
            TempData["toDataBase"] = finalView;
            return View(finalView);
        }
 
        //Legger alt inn i database
        [HttpPost]
        public ActionResult pushDatabase(ViewModel incard)
        {
            var toPush = (ViewModel)TempData["toDataBase"];
            toPush.cardInfo = incard.cardInfo;
            if (ModelState.IsValid)
            {
                var db = new DBWebApp();
                bool pushOK = db.pushToDataBase(toPush);
                if (pushOK)
                {
                    return RedirectToAction("FinishedBooking");
                }
            }
            return RedirectToAction("Error");
        }
        
        //Viser view som bekrefter bestillingen
        public ActionResult FinishedBooking()
        {
            return View();
        }

        //View som viser når noe går galt
        public ActionResult Error()
        {
            return View();
        }

    }


    
}