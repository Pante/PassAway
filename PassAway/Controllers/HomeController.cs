using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using PassAway.Models;

using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace PassAway.Controllers {

    [AllowAnonymous]
    public class HomeController : Controller {

        private UserManager<User> users;


        public HomeController(UserManager<User> users) {
            this.users = users;
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