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
        private RoleManager<IdentityRole> roles;


        public NavigationBarViewComponent(IElementViewModelRepository repository, UserManager<User> users, RoleManager<IdentityRole> roles) {
            this.repository = repository;
            this.users = users;
            this.roles = roles;
        }


        public IViewComponentResult Invoke() {
            var user = users.GetUserAsync(HttpContext.User).Result;
            var names = GetRoleNames(user);

            return View(repository.Elements.Where(element => element.AllowedRoles.Any(role => names.Contains(role))));
        }

        private List<string> GetRoleNames(User user) {
            if (user != null) {
                return user.Roles.Select(role => roles.FindByIdAsync(role.RoleId).Result.Name).ToList();

            } else {
                return NONE;
            }
        }

    }

}
