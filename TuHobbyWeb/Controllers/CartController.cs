using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TuHobbyWeb.Models.Entities;
using TuHobbyWeb.Helpers;

namespace TuHobbyWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AplicationDbContext _db = new AplicationDbContext();
        // GET: CartController
        public async Task<ActionResult> Index()
        {
            int userId = User.Identity.GetId();
            var sale = await _db.Sales
                            .Include(x => x.Lines)
                            .FirstOrDefaultAsync(x => x.ConfirmedAt == null && x.CustomerId == userId);

            foreach (var item in sale.Lines)
            {
                item.Product = await _db.Products.FindAsync(item.ProductId);
            }
            return View(sale);
        }

        public async Task<ActionResult> Remove(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "El producto no fue encontrado";
                return RedirectToAction(Request.UrlReferrer?.ToString());
            }

            var line = await _db.SaleLines.FindAsync((int)id);
            if (line == null)
            {
                TempData["ErrorMessage"] = "El producto no fue encontrado";
                return RedirectToAction(Request.UrlReferrer?.ToString());
            }

            line.Quantity = line.Quantity - 1; // 0
            if (line.Quantity == 0)
            {
                _db.SaleLines.Remove(line);
                await _db.SaveChangesAsync();
            }
            else
            {
                _db.Entry(line).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }

            var lines = await _db.SaleLines
                                .Where(x => x.SaleId == line.SaleId)
                                .ToListAsync();
            Session["cart_badge"] = lines != null ? lines.Sum(x => x.Quantity) : 0; // Operador Ternario         Sum -> Suma | Count -> Cuenta    

            TempData["SuccessMessage"] = "Producto Removido correctamente";
            return Redirect(Request.UrlReferrer?.ToString());


        }

        public async Task<ActionResult> Add(int productId)
        {
            int userId = User.Identity.GetId();

            if (userId == 0)
            {
                TempData["ErrorMessage"] = "No se ha encontrado el ID del usuario conectado";
                return Redirect(Request.UrlReferrer?.ToString());
            }

            var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Producto no encontrado";
                return Redirect(Request.UrlReferrer?.ToString());
            }

            var currenSale = await _db.Sales.FirstOrDefaultAsync(x => x.ConfirmedAt == null && x.CustomerId == userId);

            if (currenSale == null)
            {
                currenSale = new Sale
                {
                    CreatedAt = DateTime.Now,
                    CustomerId = userId,
                    SubTotal = 0
                };
                _db.Sales.Add(currenSale);
                await _db.SaveChangesAsync();
            }

            var lines = await _db.SaleLines.Where(x => x.SaleId == currenSale.SaleId).ToListAsync();

            var line = lines.FirstOrDefault(x => x.ProductId == productId);

            if (line != null)
            {
                if ((line.Quantity - product.ProductStock) == 0)
                {
                    TempData["ErrorMessage"] = "No quedan más unidades disponibles para este producto";
                    return Redirect(Request.UrlReferrer?.ToString());
                }

                line.Quantity = line.Quantity + 1;
                line.UpdatedAt = DateTime.Now;
                _db.Entry(line).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                Session["cart_badge"] = lines.Sum(x => x.Quantity);
                TempData["SuccessMessage"] = "Producto agregado correctamente";
                return Redirect(Request.UrlReferrer?.ToString());
            }
            else
            {
                line = new SaleLine
                {
                    ProductId = product.ProductId,
                    CreatedAt = DateTime.Now,
                    Price = product.ProductPrice,
                    Quantity = 1,
                    SaleId = currenSale.SaleId
                };
                _db.SaleLines.Add(line);
                await _db.SaveChangesAsync();
                Session["cart_badge"] = lines.Sum(x => x.Quantity);
                TempData["SuccessMessage"] = "Producto agregado correctamente";
                return Redirect(Request.UrlReferrer?.ToString());
            }
        }
    }
}