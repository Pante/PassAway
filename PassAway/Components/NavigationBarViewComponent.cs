using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;
using PassAway.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;


namespace PassAway.Components {

    [AllowAnonymous]
    public class NavigationBarViewComponent : ViewComponent {

        private static List<string> NONE = new List<string>() {""};


        private IElementViewModelRepository repository;
        private UserManager<User> users;


        public NavigationBarViewComponent(IElementViewModelRepository repository, UserManager<User> users) {
            this.repository = repository;
            this.users = users;
        }


        public IViewComponentResult Invoke() {
            var user = users.GetUserAsync(HttpContext.User).Result;
            var roles = user != null ? users.GetRolesAsync(user).Result : NONE;

            return View(repository.Elements.Where(element => element.AllowedRoles.Any(role => roles.Contains(role))));
        }

    }

}
