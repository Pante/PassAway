using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using PassAway.Models;

using System;
using System.Threading.Tasks;
using System.Globalization;

using System.Diagnostics;


namespace PassAway.Controllers {

    [Authorize(Roles = "Admins")]
    public class AccountController : MasterController {

        private UserManager<User> users;
        private RoleManager<IdentityRole> roles;
        private SignInManager<User> logins;
        private IUserValidator<User> validator;


        public AccountController(UserManager<User> users, RoleManager<IdentityRole> roles, SignInManager<User> logins, IUserValidator<User> validator) {
            this.users = users;
            this.roles = roles;
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
            var user = validate(model);
            if (user != null && ModelState.IsValid && await IsSuccessfulAsync(validator.ValidateAsync(users, user)) && await IsSuccessfulAsync(users.CreateAsync(user, model.Password)) && await IsSuccessfulAsync(users.AddToRoleAsync(user, "Customers"))) {
                await logins.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");

            } else {
                return View(model);
            }
            
        }

        private User validate(RegisterModel model) {
            bool dateValid = DateTime.TryParseExact(model.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob);
            if (!dateValid) {
                ModelState.AddModelError("", "Invalid date: " + model.DOB);
            }

            bool genderValid = model.Gender != null;
            if (!genderValid) {
                ModelState.AddModelError("", "Gender cannot be left blank");
            }

            bool passwordMatch = model.Password == model.ConfirmedPassword;
            if (!passwordMatch) {
                ModelState.AddModelError("", "Passwords do not match");
            }

            if (dateValid && genderValid && passwordMatch) {
                return new User {
                    UserName = model.Email,
                    Email = model.Email,
                    Country = model.Country,
                    Address = model.Address,
                    Gender = Genders.FromString(model.Gender),
                    DOB = dob
                };

            } else {
                return null;
            }
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

        [HttpPost]
        [Authorize(Roles = "Customers, Admins")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await logins.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }

}