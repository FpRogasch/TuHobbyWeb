using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
                CountSales = _db.Sales.Where(x => x.ConfirmedAt != null).Count(),
                CountNintendo = _db.Products.Where(x => x.PlatformId == 1).Count(),
                CountPlaystation = _db.Products.Where(x => x.PlatformId == 2).Count(),
                CountXbox = _db.Products.Where(x => x.PlatformId == 1002).Count(),
                CountPC = _db.Products.Where(x => x.PlatformId == 3).Count()
            };

            return View(vm);
        }

        // Muestra las Ventas
        public async Task<ActionResult> Sales()
        {
            var sales = await _db.Sales.ToListAsync();
            return View(sales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}