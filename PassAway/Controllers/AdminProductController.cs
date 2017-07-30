using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using PassAway.Models.Shared;


namespace PassAway.Controllers {

    [Authorize(Roles = "Administrator")]
    public class AdminProductController : Controller {

        private ProductRepository repository;


        public AdminProductController(ProductRepository repository) {
            this.repository = repository;
        }


        public ViewResult Product() {
            return View(repository.Products);
        }

        public ViewResult EditProduct(int id) {
            return View(repository.Products.FirstOrDefault(p => p.ID == id));
        }
            

        [HttpPost]
        public IActionResult EditProduct(Product product) {
            if (ModelState.IsValid) {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else {
                // Here be dragons
                return View(product);
            }
        }

    }
}
