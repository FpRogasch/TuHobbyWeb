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
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();
        // GET: Profile

        public async Task<ActionResult> Index()
        {
            int userId = User.Identity.GetId();
            var user = await _db.Users.FindAsync(userId);
            return View(user);
        }

        // Optimizar la memoria
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }

}