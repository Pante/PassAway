using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using PassAway.Models;

using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace PassAway.Controllers {
    public class HomeController : Controller {

        private UserManager<User> users;


        public HomeController(UserManager<User> users) {
            this.users = users;
        }


        public IActionResult Login() {
            return View(GetData(nameof(Login)));
        }


        [Authorize(Roles = "Administrator, Customer")]
        public IActionResult OtherAction() {
            return View("Index", GetData(nameof(OtherAction)));
        }


        private Dictionary<string, object> GetData(string action) {
            return new Dictionary<string, object> {
                ["Action"] = action,
                ["User"] = HttpContext.User.Identity.Name,
                ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
                ["In Users Role"] = HttpContext.User.IsInRole("Users"),
                ["Gender"] = CurrentUser.Result.Gender,
                ["DOB"] = CurrentUser.Result.DOB,
                ["Country"] = CurrentUser.Result.Country,
                ["Address"] = CurrentUser.Result.Address
            };
        }
        

        [Authorize]
        public async Task<IActionResult> UserProps() {
            return View(await CurrentUser);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProps( [Required] Gender Gender, [Required] DateTime DOB, [Required] string Country, [Required] string Address) {
            if (ModelState.IsValid) {
                User user = await CurrentUser;
                user.Gender = Gender;
                user.DOB = DOB;
                user.Country = Country;
                user.Address = Address;

                await users.UpdateAsync(user);
                return RedirectToAction("Index");

            } else {
                return View(await CurrentUser);
            }
        }

        private Task<User> CurrentUser => users.FindByNameAsync(HttpContext.User.Identity.Name);


        [AllowAnonymous]
        public IActionResult Index() {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact() {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About() {
            return View();
        }

    }
}