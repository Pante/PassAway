using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Models;
using Microsoft.AspNetCore.Authorization;

namespace PassAway.Controllers {

    [AllowAnonymous]
    public class ProductController : Controller {

        private ProductRepository repository;
        private int size;


        public ProductController(ProductRepository repository) {
            this.repository = repository;
            size = 5;
        }


        public ViewResult List(int page = 1) {
            return View(
                new ProductsViewModel() {
                    Products = repository.Products.OrderBy(product => product.ID).Skip((page - 1) * size).Take(size),
                    Pagination = new Pagination() {
                        TotalItems = repository.Products.Count(),
                        Current = page,
                        Size = size
                    }
                }
            );
        }

    }

}