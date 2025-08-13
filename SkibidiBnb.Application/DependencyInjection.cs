using Microsoft.Extensions.DependencyInjection;
using SkibidiBnb.Application.Features;
using SkibidiBnb.Application.Interfaces;
using SkibidiBnb.Application.Services.AuthenticationServices;
using SkibidiBnb.Application.Services.UploadCloudServices;

namespace SkibidiBnb.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IUploadCloudService, CloudinaryUploadCloudService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
