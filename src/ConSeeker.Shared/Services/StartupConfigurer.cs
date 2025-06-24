using ConSeeker.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Services
{
    public class StartupConfigurer
    {
        public static void AddShared(
            IServiceCollection services,
            IConfiguration configuration,
            string connectionStringKey)
        {
            services.AddDbContext<ConSeekerDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(connectionStringKey);
                
                options.UseSqlite(connectionString)
                    .AddInterceptors(new ForeignKeysEnablerInterceptor());
            });
        }

        public static void StartShared(IServiceProvider provider)
        {
            
        }
    }
}
