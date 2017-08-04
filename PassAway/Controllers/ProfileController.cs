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
            return View(
                //register model here
            );
        }

        public IActionResult Authenticate() {
            // return view to password page
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthenticateAsync(LoginModel model) {
            return null;
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterModel model) {
            //validate data etc
            //store it
            return null;
        }

    }
}