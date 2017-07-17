using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassAway.Models;

namespace PassAway.Controllers {
    public class HomeController : Controller {
        
        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Customer user)
        {
            if (ModelState.IsValid)
            {
                Repository.AddCustomer(user);
                return View("Login", user);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult CustomerIndex()
        {
            return View();
        }
    }
}