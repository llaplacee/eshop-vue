using DataLayer.ApplicationContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ConnectionStringExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service,IHostingEnvironment env, IConfiguration configuration)
        {
            service.AddDbContext<EshopContext>(options =>
            {
                var connectionString = "ConnectionStrings:EshopApiConnection:" + (env.IsDevelopment() ? "Development" : "Production");
                options.UseSqlServer(
                    configuration[connectionString]
                );
            });

            return service;
        }
    }
}