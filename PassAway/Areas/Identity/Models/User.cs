using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Identities {

    public enum Gender { Male, Female }

    public class User : IdentityUser<Guid> {

        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

    }

}
