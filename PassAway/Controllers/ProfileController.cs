using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;

using System.Threading.Tasks;


namespace PassAway.Controllers {

    [Authorize(Roles = "Customers, Admins")]
    public class ProfileController : Controller {

        private UserManager<User> users;
        private IPasswordHasher<User> hash;


        public ProfileController(UserManager<User> users, IPasswordHasher<User> hash) {
            this.users = users;
            this.hash = hash;
        }


        public async Task<IActionResult> Index() {
            var user = await users.GetUserAsync(HttpContext.User);
            return View(new EditProfileModel {
                Name = user.UserName,
                Gender = user.Gender.ToString(),
                DOB = user.DOB.ToString("dd/MM/yyyy"),
                Address = user.Address,
                Country = user.Country
            });
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfileAsync(EditProfileModel model) {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLoginAsync(EditLoginModel model) {
            return null;
        }

    }
}