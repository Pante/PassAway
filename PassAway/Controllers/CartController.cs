using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Models;
using PassAway.Utility;

namespace PassAway.Controllers { 

    public class CartController : Controller {

        private delegate void Operation(Cart cart, Product product);


        private ProductRepository repository;


        public CartController(ProductRepository repository) {
            this.repository = repository;
        }


        public ViewResult Index(string url) {
            return View(new CartViewModel {
                Cart = new Cart(),
                URL = url
            });
        }


        public RedirectToActionResult Add(int id, string url) {
            return Handle(id, url, (cart, product) => cart.Add(product, 1));
        }

        public RedirectToActionResult Remove(int id, string url) {
            return Handle(id, url, (cart, product) => cart.Remove(product));
        }


        private RedirectToActionResult Handle(int id, string url, Operation operation) {
            var product = repository.Products.FirstOrDefault(p => p.ID == id);
            if (product != null) {
                var cart = HttpContext.Session.GetJSON<Cart>("Cart") ?? new Cart();
                operation(cart, product);
                HttpContext.Session.SetJSON("Cart", cart);
            }

            return RedirectToAction("Index", new { url });
        }

    }


}