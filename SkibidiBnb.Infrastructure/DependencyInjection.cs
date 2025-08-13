using Microsoft.Extensions.DependencyInjection;
using SkibidiBnb.Application.Interfaces.IRepositories;
using SkibidiBnb.Infrastructure.Repositories;

namespace SkibidiBnb.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            return services;
        }
    }
}
