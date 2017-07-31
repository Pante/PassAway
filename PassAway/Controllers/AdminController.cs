using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PassAway.Extensions;
using PassAway.Models;

using System.Threading.Tasks;


namespace PassAway.Controllers {

    [Authorize(Roles = "Admins")]
    public class AdminController : MasterController {

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

                if (await IsSuccessfulAsync(users.CreateAsync(user, model.Password))) {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password) {
            var user = await users.FindByIdAsync(id);
            if (user != null) {
                user.Email = email;

                var emailValid = await IsSuccessfulAsync(userValidator.ValidateAsync(users, user));
                var passwordValid = !string.IsNullOrEmpty(password) && await IsSuccessfulAsync(passwordValidator.ValidateAsync(users, user, password));

                if (passwordValid) {
                    user.PasswordHash = hash.HashPassword(user, password);
                }

                if (emailValid && passwordValid && await IsSuccessfulAsync(users.UpdateAsync(user))) {
                    return RedirectToAction("Index");
                }

            } else {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            var user = await Exists(users.FindByIdAsync(id));

            if (user != null && await IsSuccessfulAsync(users.DeleteAsync(user))) {
                return RedirectToAction("Index");

            } else {
                return View("Index", users.Users);
            }
        }
        
    }

}