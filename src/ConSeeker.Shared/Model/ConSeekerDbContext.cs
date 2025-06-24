using ConSeeker.Shared.Model.Sources;
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

        public DbSet<Channel> Channels { get; set; }
    }
}
