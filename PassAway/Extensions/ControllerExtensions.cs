using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace PassAway.Extensions {

    public static class ControllerExtensions {

        public static void AddErrors(this Controller controller, IdentityResult result) {
            foreach (var error in result.Errors) {
                controller.ModelState.AddModelError("", error.Description);
            }
        }

    }

}
