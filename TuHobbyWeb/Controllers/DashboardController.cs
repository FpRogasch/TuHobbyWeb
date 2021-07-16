using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHobbyWeb.Helpers;
using TuHobbyWeb.Models.Entities;
using TuHobbyWeb.Models.ViewModels;

namespace TuHobbyWeb.Controllers
{
    [Authorize(Roles = StringHelper.ROLE_ADMINISTRATOR)]
    public class DashboardController : Controller
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                CountUsers = _db.Users.Count(), // Hace un select count(FirstName) from users
                CountGames = _db.Products.Count(),
                CountNintendo = _db.Products.Where(x => x.PlatformId == 1).Count(),
                CountPlaystation = _db.Products.Where(x => x.PlatformId == 2).Count(),
                CountPC = _db.Products.Where(x => x.PlatformId == 3).Count()
            };

            return View(vm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}