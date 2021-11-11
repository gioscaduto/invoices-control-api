using InvoicesControl.Api.Extensions;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InvoicesControl.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // JWT
            var appSettingsSections = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSections);

            var appSettings = appSettingsSections.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = true;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidUrls,
                    ValidIssuer = appSettings.Issuer
                };
            });

            return services;
        }
    }
}
