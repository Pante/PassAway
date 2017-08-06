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

    public class AccountController : MasterController {

        private UserManager<User> users;
        private RoleManager<IdentityRole> roles;
        private SignInManager<User> logins;
        private IUserValidator<User> userValidator;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> hash;


        public AccountController(UserManager<User> users, RoleManager<IdentityRole> roles, SignInManager<User> logins, IUserValidator<User> userValidator, IPasswordValidator<User> passwordValidator, IPasswordHasher<User> hash) {
            this.users = users;
            this.roles = roles;
            this.logins = logins;
            this.userValidator = userValidator;
            this.passwordValidator = passwordValidator;
            this.hash = hash;
        }


        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model) {
            var user = Validate(model);
            if (user != null && ModelState.IsValid && await IsSuccessfulAsync(userValidator.ValidateAsync(users, user)) && await IsSuccessfulAsync(users.CreateAsync(user, model.Password)) && await IsSuccessfulAsync(users.AddToRoleAsync(user, "Customers"))) {
                await logins.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");

            } else {
                return View(model);
            }      
        }

        private User Validate(RegisterModel model) {
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


        [Authorize(Roles = "Customers, Admins")]
        public IActionResult Validate() {
            return View(new PasswordModel());
        }

        [HttpPost]
        [Authorize(Roles = "Customers, Admins")]
        public async Task<IActionResult> Validate(PasswordModel model) {
            if (ModelState.IsValid) {
                var user = await users.GetUserAsync(HttpContext.User);

                if (user != null && model.Password != null & hash.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success) {
                    return RedirectToAction("Profile");

                } else {
                    ModelState.AddModelError(nameof(PasswordModel), "Invalid password");
                }
            }

            return View();
        }


        [Authorize(Roles = "Customers, Admins")]
        public async Task<IActionResult> Profile() {
            var user = await users.GetUserAsync(HttpContext.User);
            if (user != null) {
                return View(new RegisterModel {
                    Name = user.UserName,
                    Email = user.Email,
                    Gender = user.Gender.ToString(),
                    DOB = user.DOB.ToString(),
                    Address = user.Address,
                    Country = user.Country
                });

            } else {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customers, Admins")]
        public async Task<IActionResult> EditProfile(RegisterModel model) {
            var user = await users.GetUserAsync(HttpContext.User);
            var updated = Validate(model);

            if (updated != null) {
                user.UserName = updated.UserName;
                user.Gender = updated.Gender;
                user.Country = updated.Country;
                user.Address = updated.Address;

                await users.UpdateAsync(user);
                return RedirectToAction("Index", "Home");

            } else {
                return RedirectToAction("Profile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customers, Admins")]
        public async Task<IActionResult> EditPassword(RegisterModel model) {
            var user = await users.GetUserAsync(HttpContext.User);
   
            if (user != null && model.Password != null && model.Password == model.ConfirmedPassword && await IsSuccessfulAsync(passwordValidator.ValidateAsync(users, user, model.Password))) {
                user.PasswordHash = hash.HashPassword(user, model.Password);

                var result = await users.UpdateAsync(user);
                if (result.Succeeded) {
                    Debug.WriteLine("Ops");
                } else {
                    Debug.WriteLine("OKkkkk");
                }

                return RedirectToAction("Index", "Home");

            } else {
                foreach (var state in ModelState.Values) {
                    foreach(var error in state.Errors) {
                        Debug.WriteLine("Error: " + error.ErrorMessage);
                    }
                }
                return RedirectToAction("Profile");
            }
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