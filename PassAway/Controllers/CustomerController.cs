using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PassAway.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerIndex()
        {
            return View();
        }

        public ActionResult DisplayProducts()
        {
            return View();
        }

    }
}