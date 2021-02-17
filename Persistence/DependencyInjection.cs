using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BudgetDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()                       
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IBudgetPeriodRepository, BudgetPeriodRepository>();
            services.AddScoped<IBudgetYearRepository, BudgetYearRepository>();


            return services;
        }
    }
}
