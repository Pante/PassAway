using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Shared {
    public class ApplicationContext : DbContext {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {

        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
