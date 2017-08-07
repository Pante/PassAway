using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Models;

using System.Linq;
using System.Diagnostics;

namespace PassAway.Controllers {

    [Authorize(Roles = "Customers, Admins")]
    public class ProductController : Controller {

        private ProductRepository products;
        private int size;


        public ProductController(ProductRepository products, int size = 5) {
            this.products = products;
            this.size = 5;
        }


        public ViewResult List(int page = 1) {
            return View(
                new ProductsViewModel() {
                    Products = products.Products.OrderBy(product => product.ID).Where(product => product.Stock > 0),
                    Pagination = new Pagination() {
                        TotalItems = products.Products.Count(),
                        Current = page,
                        Size = size
                    }
                }
            );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PurchaseClick(Product product) {
            return View("~/Views/Product/Purchase.cshtml", new PurchaseViewModel() {
                ID = product.ID.ToString(),
                Quantity = "1"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Purchase(PurchaseViewModel model) {
            var product = products.Products.First(p => p.ID == int.Parse(model.ID));
            var quantityValid = int.TryParse(model.Quantity, out int quantity);

            if (product != null && product.Stock >= quantity) {
                product.Stock -= quantity;
                products.SaveProduct(product);
                return RedirectToAction("Index", "Home");

            } else {
                return View(model);
            }
        }

    }

}