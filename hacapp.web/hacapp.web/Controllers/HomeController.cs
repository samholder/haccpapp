using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacapp.web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (ControllerContext.HttpContext.Request.IsAuthenticated)
            {
                ViewBag.Message = "Welcome back " + ControllerContext.HttpContext.User.Identity.Name;
                return View();
            }

            ViewBag.Message = "Welcome to HACCAPP. Please log in to continue.";
            return RedirectToAction("Account/Login");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}