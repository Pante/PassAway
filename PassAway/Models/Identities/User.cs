using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Identities {
    public class User : IdentityUser<Guid> {

        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

    }


    public enum Gender {

        MALE, FEMALE;

    }

}
