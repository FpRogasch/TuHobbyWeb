using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    public class PlatformsController : DefaultBaseController
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();
        // GET: Platforms
        public async Task<ActionResult> Index()
        {
            var platforms = await _db.ProductPlatforms.Include("Products").ToListAsync();
            return View(platforms);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new PlatformCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlatformCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ProductPlatform model = new ProductPlatform();
                model.ProductPlatformName = vm.ProductPlatformName;
                model.ProductPlatformLogo = UploadFile(vm.PlatformFile, "plataformas");
                _db.ProductPlatforms.Add(model);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Plataforma creada correctamente.";
                return RedirectToAction("Index");
            }

            return View(vm);
        }
    }
}