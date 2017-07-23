using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PassAway.Models
{

    public enum Gender { Male, Female }

    public class AppUser : IdentityUser
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