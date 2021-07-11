using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

    public class ProductController : DefaultBaseController
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();
        // GET: Product
        public async Task<ActionResult> Index(ProductIndexViewModel vm)
        {
            vm.Products = await GetProducts(vm);

            vm.Platforms = await _db.ProductPlatforms
                                    .OrderBy(x => x.ProductPlatformName)
                                    .ToListAsync();

            return View(vm);
        }

        public async Task<List<Product>> GetProducts(ProductIndexViewModel vm)
        {
            var queryProduct = _db.Products.AsQueryable();

            if (vm.ProductCode != null)
                queryProduct = queryProduct.Where(x => x.ProductCode == vm.ProductCode);
            if (vm.ProductName != null)
                queryProduct = queryProduct.Where(x => x.ProductName == vm.ProductName);
            if (vm.PlatformId != null)
                queryProduct = queryProduct.Where(x => x.PlatformId == vm.PlatformId);

            switch (vm.Sort)
            {
                case 1:
                    queryProduct = queryProduct.OrderBy(x => x.ProductCode);
                    break;
                case -1:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductCode);
                    break;
                case 2:
                    queryProduct = queryProduct.OrderBy(x => x.ProductName);
                    break;
                case -2:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductName);
                    break;
                case 3:
                    queryProduct = queryProduct.OrderBy(x => x.ProductPrice);
                    break;
                case -3:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductPrice);
                    break;
                case 4:
                    queryProduct = queryProduct.OrderBy(x => x.ProductStock);
                    break;
                case -4:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductStock);
                    break;
                default:
                    queryProduct = queryProduct.OrderBy(x => x.ProductName);
                    break;
            }

            return await queryProduct.ToListAsync();
        }

        // Mostrar el Producto
        [HttpGet]
        public async Task<ActionResult> Show(int productId)
        {
            var product = await _db.Products.FindAsync(productId);

            if(product == null)
            {
                TempData["ErrorMessage"] = "El producto no fue encontrado";
                return RedirectToAction("Index");
            }

            var vm = await GetCreateViewModel(product);

            return View(vm);
        }

        // Crear Producto en la DB
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var vm = await GetCreateViewModel();
            return View(vm);
        }

        public async Task<ProductCreateViewModel> GetCreateViewModel(Product product = null)
        {
            var vm = new ProductCreateViewModel();
            vm.ProductPlatforms = await _db.ProductPlatforms.OrderBy(x => x.ProductPlatformName).ToListAsync();
            
            if (product != null)
            {
                vm.Product = product;
            }

            return vm;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductCode == model.ProductCode);
                if (product != null)
                {
                    TempData["ErrorMessage"] = "El código del Producto ya se encuentra registrado";
                    return View(model);
                }
                product = new Product
                {
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    ProductPrice = (int)model.ProductPrice,
                    ProductStock = (int)model.ProductStock,
                    PlatformId = model.PlatformId
                };

                product.ProductImage = UploadFile(model.ProductFile, "products");

                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El Producto ha sido creado correctamente";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Modificar Producto en la DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductId == null)
                {
                    TempData["ErrorMessage"] = "Imposible actualizar el Producto";
                    return RedirectToAction("Index");
                }

                var product = await _db.Products.FindAsync(model.ProductId);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "El Producto no se encuentra registrado, imposible actualizar";
                    return RedirectToAction("Index");
                }

                product.ProductCode = model.ProductCode;
                product.ProductName = model.ProductName;
                product.ProductPrice = (int)model.ProductPrice;
                product.ProductStock = (int)model.ProductStock;


                _db.Products.AddOrUpdate(product);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El Producto ha sido actualizado correctamente.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Show", model.ProductId);
        }

        public async Task<ActionResult> Delete(int? productId)
        {
            if (productId == null)
            {
                TempData["ErrorMessage"] = "Imposible eliminar el Producto.";
                return RedirectToAction("Index");
            }

            var product = await _db.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "El Producto no se ha encontrad, imposible eliminar.";
                return RedirectToAction("Index");
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "El Producto se ha elimnado correctamente.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            { 
                _db.Dispose(); 
            }
            base.Dispose(disposing);
        }

    }
}