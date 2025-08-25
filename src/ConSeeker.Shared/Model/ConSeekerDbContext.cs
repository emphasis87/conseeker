using ConSeeker.Shared.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model
{
    public class ConSeekerDbContext : DbContext
    {
        protected ConSeekerDbContext()
        {
        }

        public ConSeekerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Provider> Conventions { get; set; }
        public DbSet<Provider> Activities { get; set; }
        public DbSet<Provider> Hosts { get; set; }
    }
}
