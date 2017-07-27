using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PassAway.Models
{

    public enum Gender { M, F }

    public static class Genders {

        public static Gender FromString(string value) {
            value = value.ToLower();
            if (value == "m") {
                return Gender.M;

            } else {
                return Gender.F;
            }
        }

    }



    public class User : IdentityUser
    {

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> customer)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await customer.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

    }

    }