using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.appTitle = "Dashboard";
            ViewBag.appSubtitle = "PTT SmartCity Web API";
            return View();
        }
    }
}
