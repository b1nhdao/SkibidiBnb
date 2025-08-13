using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkibidiBnb.Application;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Infrastructure;
using SkibidiBnb.Infrastructure.Data;
using SkibidiBnb.Infrastructure.Repositories;


namespace SkibidiBnb.Api.Bootstrapping
{
    public static class AppServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<SkibidiBnbDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SkibidiBnbDb"));
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://localhost:7066",
                    ValidAudience = "https://localhost:7066",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your-256-bit-secret-your-256-bit-secret"))
                };
            });

            // Cloudinary configuration
            Account account = new Account(
                builder.Configuration["Cloudinary:CloudName"],
                builder.Configuration["Cloudinary:ApiKey"],
                builder.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            
            builder.Services.AddSingleton(cloudinary);

            // Register application services
            builder.Services.AddApplicationDI();

            // Register repositories
            builder.Services.AddInfrastructureDI();
        }
    }
}
