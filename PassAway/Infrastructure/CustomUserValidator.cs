using Microsoft.AspNetCore.Identity;

using PassAway.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PassAway.Infrastructure {

    public class CustomUserValidator : UserValidator<User> {

        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user) {
            var result = await base.ValidateAsync(manager, user);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (DateTime.Today < user.DOB) {
                errors.Add(new IdentityError {
                    Code = "PersonFromTheFuture",
                    Description = "You aren't from the future. DOB cannot be greater than today"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}