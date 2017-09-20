using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration(Booking newFlightBooking)
        {
                var db = new DBWebApp();
                FinalBooking finalBooking = new FinalBooking();
                finalBooking.booking = db.flightRegistration(newFlightBooking);
                return View(finalBooking);
        }
    }
}