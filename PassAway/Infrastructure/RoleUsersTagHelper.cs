﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;

using PassAway.Models;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace Users.Infrastructure {

    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper {

        private UserManager<User> users;
        private RoleManager<IdentityRole> roles;


        public RoleUsersTagHelper(UserManager<User> users, RoleManager<IdentityRole> roles) {
            this.users = users;
            this.roles = roles;
        }


        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            var names = new List<string>();
            var role = await roles.FindByIdAsync(Role);

            if (role != null) {
                foreach (var user in users.Users) {
                    if (user != null && await users.IsInRoleAsync(user, role.Name)) {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}
