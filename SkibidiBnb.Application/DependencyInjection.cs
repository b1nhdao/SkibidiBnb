using Microsoft.Extensions.DependencyInjection;
using SkibidiBnb.Application.Features.Authentication.Services;
using SkibidiBnb.Application.Features.Property.Services;
using SkibidiBnb.Application.Features.User.Services;
using SkibidiBnb.Application.SharedServices.Jwt;
using SkibidiBnb.Application.SharedServices.UploadCloud;

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
