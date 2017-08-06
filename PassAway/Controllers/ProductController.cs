using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Models;

using System.Linq;
using System.Diagnostics;

namespace PassAway.Controllers {

    [AllowAnonymous]
    public class ProductController : Controller {

        private ProductRepository products;
        private int size;


        public ProductController(ProductRepository products, int size = 5) {
            this.products = products;
            this.size = 5;
        }


        public ViewResult List(int page = 1) {
            Debug.WriteLine("COUNT: " + products.Products.Count());
            return View(
                new ProductsViewModel() {
                    Products = products.Products.OrderBy(product => product.ID).Skip((page - 1) * size).Take(size),
                    Pagination = new Pagination() {
                        TotalItems = products.Products.Count(),
                        Current = page,
                        Size = size
                    }
                }
            );
        }

    }

}