using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;

using System.Threading.Tasks;


namespace PassAway.Controllers {

    public class MasterController : Controller  {

        protected async Task<User> Exists(Task<User> task) {
            var user = await task;
            if (user == null) {
                ModelState.AddModelError("", "User Not Found");
            }

            return user;
        }

        protected async Task<IdentityRole> Exists(Task<IdentityRole> task) {
            var role = await task;
            if (role == null) {
                ModelState.AddModelError("", "Role Not Found");
            }

            return role;
        }


        protected async Task<bool> IsSuccessfulAsync(Task<IdentityResult> task) {
            var result = await task;
            if (!result.Succeeded) {
                AddErrors(result);
            }

            return result.Succeeded;
        }

        protected void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}
