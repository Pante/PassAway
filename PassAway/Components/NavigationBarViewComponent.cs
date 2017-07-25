using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;
using PassAway.Models.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Components {

    [AllowAnonymous]
    public class NavigationBarViewComponent : ViewComponent {

        private IElementViewModelRepository repository;
        private UserManager<AppUser> users;
 

        public NavigationBarViewComponent(IElementViewModelRepository repository, UserManager<AppUser> users) {
            this.repository = repository;
            this.users = users;
        }


        public IViewComponentResult Invoke() {
            var user = users.GetUserAsync(HttpContext.User);
            var roles = user.Result.Roles.Select(role => role.RoleId).ToList();

            return View(repository.Elements.Where(element => roles.Any(role => element.AllowedRoles.Contains(role))));
        }

    }

}
