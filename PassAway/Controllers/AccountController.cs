﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using PassAway.Models;

using System;
using System.Threading.Tasks;
using System.Globalization;

namespace PassAway.Controllers {

    [Authorize(Roles = "Administrator")]

    public class AccountController : MasterController {

        private UserManager<User> users;
        private SignInManager<User> logins;
        private IUserValidator<User> validator;


        public AccountController(UserManager<User> users, SignInManager<User> logins, IUserValidator<User> validator) {
            this.users = users;
            this.logins = logins;
            this.validator = validator;
        }


        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model) {
            if (DateTime.TryParseExact(model.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob)) {
                User user = new User {
                    UserName = model.Email,
                    Email = model.Email,
                    Country = model.Country,
                    Address = model.Address,
                    Gender = Genders.FromString(model.Gender),
                    DOB = dob
                };

                if (ModelState.IsValid && await IsSuccessfulAsync(validator.ValidateAsync(users, user)) && await IsSuccessfulAsync(users.CreateAsync(user, model.Password))) {
                    await logins.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            } else {
                ModelState.AddModelError("", "Invalid date, " + model.DOB);
            }

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl) {
            if (ModelState.IsValid) {
                var user = await users.FindByEmailAsync(details.Email);
                if (user != null) {
                    await logins.SignOutAsync();
                    if (logins.PasswordSignInAsync(user, details.Password, false, false).Result.Succeeded) {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }

            return View(details);
        }


        public async Task<IActionResult> Logout() {
            await logins.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public IActionResult AccessDenied() {
            return View();
        }

    }

}