using Microsoft.AspNetCore.Mvc;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Models;
using PassAway.Extensions;

using System.Linq;
using System;

namespace PassAway.Controllers {

    public class CartController : Controller {

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


        private RedirectToActionResult Handle(int id, string url, Action<Cart, Product> action) {
            var product = repository.Products.FirstOrDefault(p => p.ID == id);
            if (product != null) {
                var cart = HttpContext.Session.GetJSON<Cart>("Cart") ?? new Cart();
                action(cart, product);
                HttpContext.Session.SetJSON("Cart", cart);
            }

            return RedirectToAction("Index", new { url });
        }

    }


}