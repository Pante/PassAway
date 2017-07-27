using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassAway.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using PassAway.Models.Shared;


namespace PassAway.Controllers
{
    class AdminProductController : Controller
    {
        private ProductRepository repository;

        public AdminProductController(ProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Product() => View(repository.Products);

        public ViewResult EditProduct(int productId) =>
            View(repository.Products
            .FirstOrDefault(p => p.ID == productId));


        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

    }
}
