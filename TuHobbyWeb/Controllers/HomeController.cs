using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TuHobbyWeb.DAL;
using TuHobbyWeb.Models.Entities;
using TuHobbyWeb.Models.ViewModels;

namespace TuHobbyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();
        public async Task<ActionResult> Index(ProductIndexViewModel vm)
        {
            vm.Products = await ProductDAL.GetProducts(vm, _db);

            vm.Platforms = await _db.ProductPlatforms.OrderBy(x => x.ProductPlatformName).ToListAsync();

            return View(vm);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}