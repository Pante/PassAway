﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using PassAway.Extensions;
using PassAway.Models;

using System;
using System.Threading.Tasks;


namespace PassAway.Controllers {

    [Authorize(Roles = "Administrator")]

    public class AccountController : MasterController {

        private UserManager<User> users;
        private SignInManager<User> logins;


        public AccountController(UserManager<User> users, SignInManager<User> logins) {
            this.users = users;
            this.logins = logins;
        }


        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model) {
            User user = null;
            if (ModelState.IsValid && await IsSuccessfulAsync(users.CreateAsync(user = From(model), model.Password)))  {
                await logins.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");

            } else { 
                return View(model);
            }
        }

        private User From(RegisterModel model) {
            return new User {
                UserName = model.Email,
                Email = model.Email,
                Country = model.Country,
                Address = model.Address,
                Gender = Genders.FromString(model.Gender),
                DOB = Convert.ToDateTime(model.DOB)
            };
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