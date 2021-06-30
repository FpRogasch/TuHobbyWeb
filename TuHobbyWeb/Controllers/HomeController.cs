using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHobbyWeb.Helpers;

namespace TuHobbyWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole(StringHelper.ROLE_ADMINISTRATOR))
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
        }

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

        [Authorize]
        public ActionResult Private()
        {
            return RedirectToAction("About");
        }
    }
}