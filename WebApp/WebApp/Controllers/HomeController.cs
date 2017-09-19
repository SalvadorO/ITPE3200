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
        public ActionResult Registration(Booking inBooking)
        {
            var db = new DBWebApp();
            bool OK = db.flight(inBooking);
            if (OK)
            {
                FinalBooking finalBooking = new FinalBooking();
                finalBooking.booking = inBooking;
                return View(finalBooking);
            }
            return View();
        }
    }
}