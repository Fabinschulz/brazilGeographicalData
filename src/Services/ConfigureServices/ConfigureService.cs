using BrazilGeographicalData.src.Application.IdentityData;
using BrazilGeographicalData.src.Infra.Repositories;
using BrazilGeographicalData.src.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BrazilGeographicalData.src.Services.ConfigureServices
{
    public static class ConfigureService
    {
        public static void AddUserContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<UserRepository>();
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddAuthJwt(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var key = Encoding.ASCII.GetBytes("MySecretKey");
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void AddSwaggerDoc(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new()
                {
                    Title = "Dados Geográficos do Brasil (IBGE)",
                    Version = "v1",
                    Description = "Uma API para consultar dados geográficos do Brasil."

                });
            });
        }

        public static void AddAuthPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole(IdentityData.ADMIN));
                opt.AddPolicy("User", policy => policy.RequireRole(IdentityData.USER));
            });
        }
    }

}
