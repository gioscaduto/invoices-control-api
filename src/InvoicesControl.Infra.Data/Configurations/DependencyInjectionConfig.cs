using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoicesControl.Infra.Data.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                            .Assembly.FullName)));

            return services;
        }
    }
}
