using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Population.SQLite.App.Application.Abstracts;
using Population.SQLite.App.Application.Common.Interfaces;
using Population.SQLite.App.Infrastructure.Persistence;
using Population.SQLite.App.Infrastructure.Repositories;
using System.IO;

namespace Population.SQLite.App.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string dirname = Directory.GetCurrentDirectory();
            string cs = configuration.GetConnectionString("cs").Replace("Rootpath", dirname);
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlite(cs));

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddTransient(typeof(IPopulationRepository), typeof(PopulationRepository));
            services.AddTransient(typeof(IHouseholdRepository), typeof(HouseholdRepository));

            return services;
        }
    }
}
