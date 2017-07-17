using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PassAway.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Login> LoginRegisters { get; set; }
        public DbSet<Register> RegisterRecords { get; set; }
    }
}