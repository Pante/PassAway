using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using System;
using System.Globalization;
using PassAway.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PassAway.Controllers {

    [Authorize(Roles = "Admins")]
    public class AdminProductController : Controller {

        private ProductRepository repository;


        public AdminProductController(ProductRepository repository) {
            this.repository = repository;
        }


        public ViewResult List(int page = 1) {
            return View(
                new ProductsViewModel() {
                    Products = repository.Products.OrderBy(product => product.ID),
                    Pagination = new Pagination() {
                        TotalItems = repository.Products.Count(),
                        Current = page,
                        Size = 5
                    }
                }
            );
        }

        public ViewResult Create() {
            return View(new CreateProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(CreateProductViewModel model) {
            DateTime produced;
            bool dateValid = DateTime.TryParseExact(model.Produced, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out produced);
            if (!dateValid || produced > DateTime.Today) {
                ModelState.AddModelError("", "Invalid date: " + model.Produced);
            }

            decimal price;
            bool PriceValid = Decimal.TryParse(model.Price, out price);
            if (!PriceValid || price < 0) {
                ModelState.AddModelError("", "Invalid price");
            }

            bool stockValid = int.TryParse(model.Stock, out int stock);
            if (!stockValid || stock < 0) {
                ModelState.AddModelError("", "Invalid stock");
            }

            Debug.WriteLine("value: " + model.Promotion);
            bool promotionValid = bool.TryParse(model.Promotion, out bool promotion);
            if (!promotionValid) {
                ModelState.AddModelError("", "Invalid promotional");
            }

            if (ModelState.IsValid && dateValid && PriceValid && stockValid && promotionValid) {
                repository.SaveProduct(new Product() {
                    Name = model.Name,
                    Description = model.Description,
                    URL = model.URL,
                    Produced = produced,
                    Price = price,
                    Stock = stock,
                    Promotion = promotion,
                    Ratings = new List<Rating>()
                });

                return RedirectToAction("List", "Product");

            } else {
                foreach(var state in ModelState.Values) {
                    foreach(var error in state.Errors) {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
                return RedirectToAction("Create");
            }

        }



        public ViewResult Edit(Product product) {
            return View("~/Views/AdminProduct/Edit.cshtml", new EditProductViewModel() {
                ID = product.ID.ToString(),
            });
        }
            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(EditProductViewModel model) {
            DateTime produced;
            bool dateValid = DateTime.TryParseExact(model.Produced, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out produced);
            if (!dateValid || produced > DateTime.Today) {
                ModelState.AddModelError("", "Invalid date: " + model.Produced);
            }

            decimal price;
            bool PriceValid = Decimal.TryParse(model.Price, out price);
            if (!PriceValid || price < 0) {
                ModelState.AddModelError("", "Invalid price");
            }

            bool stockValid = int.TryParse(model.Stock, out int stock);
            if (!stockValid || stock < 0) {
                ModelState.AddModelError("", "Invalid stock");
            }

            Debug.WriteLine("value: " + model.Promotion);
            bool promotionValid = bool.TryParse(model.Promotion, out bool promotion);
            if (!promotionValid) {
                ModelState.AddModelError("", "Invalid promotional");
            }

            if (ModelState.IsValid && dateValid && PriceValid && stockValid && promotionValid) {
                repository.SaveProduct(new Product() {
                    ID = int.Parse(model.ID),
                    Name = model.Name,
                    Description = model.Description,
                    URL = model.URL,
                    Produced = produced,
                    Price = price,
                    Stock = stock,
                    Promotion = promotion,
                    Ratings = new List<Rating>()
                });

                return RedirectToAction("List", "AdminProduct");

            } else {
                foreach (var state in ModelState.Values) {
                    foreach (var error in state.Errors) {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
                return RedirectToAction("Edit", new { ID = model.ID});
            }
        }
        

    }
}
