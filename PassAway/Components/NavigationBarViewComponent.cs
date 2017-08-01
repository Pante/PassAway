using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;
using PassAway.Models.ViewModels;

using System.Linq;


namespace PassAway.Components {

    [AllowAnonymous]
    public class NavigationBarViewComponent : ViewComponent {

        private ElementViewModelRepository repository;
        private UserManager<User> users;


        public NavigationBarViewComponent(ElementViewModelRepository repository, UserManager<User> users) {
            this.repository = repository;
            this.users = users;
        }


        public IViewComponentResult Invoke() {
            var user = users.GetUserAsync(HttpContext.User).Result;
            var roles = user != null ? users.GetRolesAsync(user).Result : ElementViewModel.ANONYMOUS;

            return View(repository.Elements.Where(element => element.AllowedRoles.Any(role => roles.Contains(role))));
        }
    }

}
