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

        [HttpPost]
        public ActionResult Registration(Booking inFlight)
        {
            var db = new DBWebApp();
            bool OK = db.flight(inFlight);
            if (OK)
            {
                return View(inFlight);
            }
            return View();
        }
    }
}