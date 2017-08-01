using Microsoft.AspNetCore.Identity;

using PassAway.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;


namespace PassAway.Infrastructure {

    public class CustomPasswordValidator : PasswordValidator<User> {

        public override async Task<IdentityResult> ValidateAsync(UserManager<User> users, User user, string password) {
            var result = await base.ValidateAsync(users, user, password);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (password.ToLower().Contains(user.UserName.ToLower())) {
                errors.Add(new IdentityError {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain username"
                });
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$")) {
                errors.Add(new IdentityError {
                    Code = "PasswordInvalidCharacter",
                    Description = "Password can only contains letters and numbers"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success: IdentityResult.Failed(errors.ToArray());
        }

    }

}