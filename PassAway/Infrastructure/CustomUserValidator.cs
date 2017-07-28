using Microsoft.AspNetCore.Identity;

using PassAway.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PassAway.Infrastructure {

    public class CustomUserValidator : UserValidator<User> {

        private const string MAGICAL_REGEX = "? (\")(\".+? (?< !\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";


        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user) {
            var result = await base.ValidateAsync(manager, user);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (!Regex.IsMatch(user.Email, MAGICAL_REGEX)) {
                errors.Add(new IdentityError {
                    Code = "EmailDomainError",
                    Description = "Invalid domain name"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}