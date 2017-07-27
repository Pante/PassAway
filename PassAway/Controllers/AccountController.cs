using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using PassAway.Extensions;
using PassAway.Models;

using System;
using System.Threading.Tasks;


namespace PassAway.Controllers {

    public class AccountController : Controller {

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
            if (ModelState.IsValid) {
                var user = new User {
                    UserName = model.Email,
                    Email = model.Email,
                    Country = model.Country,
                    Address = model.Address,
                    Gender = Genders.FromString(model.Gender),
                    DOB = Convert.ToDateTime(model.DOB)
                };

                var result = await users.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    await logins.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");

                } else {
                    this.AddErrors(result);
                }
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