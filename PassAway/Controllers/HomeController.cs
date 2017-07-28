using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PassAway.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace PassAway.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        private UserManager<User> userManager;
        public HomeController(UserManager<User> userMgr)
        {
            userManager = userMgr;
        }


        [Authorize]
        public IActionResult Login() => View(GetData(nameof(Login)));

        [Authorize(Roles = "Customer")]
        public IActionResult OtherAction() => View("Index",
        GetData(nameof(OtherAction)));

        private Dictionary<string, object> GetData(string actionName) =>
        new Dictionary<string, object>
        {
            ["Action"] = actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In Users Role"] = HttpContext.User.IsInRole("Users"),
            ["Gender"] = CurrentUser.Result.Gender,
            ["DOB"] = CurrentUser.Result.DOB,
            ["Country"] = CurrentUser.Result.Country,
            ["Address"] = CurrentUser.Result.Address
        };


        [Authorize]
        public async Task<IActionResult> UserProps()
        {
            return View(await CurrentUser);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProps(
        [Required]Gender Gender,
        [Required]DateTime DOB,
        [Required]string Country,
        [Required]String Address)
        {
            if (ModelState.IsValid)
            {
                User user = await CurrentUser;
                user.Gender = Gender;
                user.DOB = DOB;
                user.Country = Country;
                user.Address = Address;

                await userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(await CurrentUser);
        }
        private Task<User> CurrentUser =>
        userManager.FindByNameAsync(HttpContext.User.Identity.Name);


    }
}