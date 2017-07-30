using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System;


namespace PassAway.Models {

    public class AppIdentityDbContext : IdentityDbContext<User> {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }


        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration) {
            UserManager<User> users = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roles = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var username = configuration["Data:AdminUser:Name"];
            var email = configuration["Data:AdminUser:Email"];
            var password = configuration["Data:AdminUser:Password"];
            var role = configuration["Data:AdminUser:Role"];

            if (await users.FindByNameAsync(username) == null) {

                if (await roles.FindByNameAsync(role) == null) {
                    await roles.CreateAsync(new IdentityRole(role));
                }

                User user = new User {
                    UserName = username,
                    Email = email
                };

                if ((await users.CreateAsync(user, password)).Succeeded) {
                    await users.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
