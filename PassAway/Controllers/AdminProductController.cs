using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;

namespace PassAway.Controllers {

    [Authorize(Roles = "Admins")]
    public class AdminProductController : Controller {

        private ProductRepository repository;


        public AdminProductController(ProductRepository repository) {
            this.repository = repository;
        }


        public ViewResult Create() {
            return View(new CreateProductViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model) {

        }



        public ViewResult Edit(int id) {
            return View(repository.Products.FirstOrDefault(p => p.ID == id));
        }
            

        [HttpPost]
        public IActionResult Edit(Product product) {
           
            else {
                // Here be dragons
                return View(product);
            }
        }
        

    }
}
