using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace PassAway.Models {

    public class CreateModel {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }


    public class LoginModel {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }


    public class EditProfileModel {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

    }

    public class EditLoginModel {

        [Required]
        public string Email { get; set; }
        [Required]
        public string NewPasword { get; set; }
        [Required]
        public string NewPasswordConfirmed { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmedPassword { get; set; }
    }


    public class PasswordModel {

        public string Password { get; set; }

    }


    public class RoleEditModel {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }


    public class RoleModificationModel {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }


    public class RegisterModel {
     
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }


        public string Gender { get; set; }
        
        public string DOB { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }

    }


    public class ParentUsermodel {
        public RegisterModel register { get; set; }
        public LoginModel  login { get; set; }
    }

}