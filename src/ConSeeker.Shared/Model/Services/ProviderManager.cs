using ConSeeker.Shared.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Services
{
    public interface IProviderManager
    {
        Task<IEnumerable<Provider>> GetProvidersAsync();
    }

    public class ProviderManager : IProviderManager
    {
        private readonly ConSeekerDbContext db;

        public ProviderManager(ConSeekerDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Provider>> GetProvidersAsync()
        {
            var providers = await db.Providers.ToListAsync();
            return providers;
        }
    }
}
