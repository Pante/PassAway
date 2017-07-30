using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PassAway.Extensions;
using PassAway.Models;
using PassAway.Models.Shared;

using System.Linq;

using System.Threading.Tasks;


namespace PassAway.Controllers {

    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller {

        private UserManager<User> users;
        private IUserValidator<User> userValidator;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> hash;


        public AdminController(UserManager<User> users, IUserValidator<User> userValidator, IPasswordValidator<User> passwordValidator, IPasswordHasher<User> hash) {
            this.users = users;
            this.userValidator = userValidator;
            this.passwordValidator = passwordValidator;
            this.hash = hash;
        }


        public ViewResult Index() {
            return View(users.Users);
        }


        public ViewResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model) {
            if (ModelState.IsValid) {
                var user = new User {
                    UserName = model.Name,
                    Email = model.Email
                };

                var result = await users.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index");

                } else {
                    this.AddErrors(result);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password) {
            var user = await users.FindByIdAsync(id);
            if (user != null) {
                user.Email = email;
                var validEmail = await userValidator.ValidateAsync(users, user);
                if (!validEmail.Succeeded) {
                    this.AddErrors(validEmail);
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password)) {
                    validPass = await passwordValidator.ValidateAsync(users, user, password);

                    if (validPass.Succeeded) {
                        user.PasswordHash = hash.HashPassword(user, password);

                    } else {
                        this.AddErrors(validEmail);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded)) {
                    var result = await users.UpdateAsync(user);
                    if (result.Succeeded) {
                        return RedirectToAction("Index");

                    } else {
                        this.AddErrors(result);
                    }
                }

            } else {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            var user = await users.FindByIdAsync(id);
            if (user != null) {
                var result = await users.DeleteAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Index");

                } else {
                    this.AddErrors(result);
                }

            } else {
                ModelState.AddModelError("", "User Not Found");
            }

            return View("Index", users.Users);
        }
        
    }

}