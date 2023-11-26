using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Behaviors;
using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using BrazilGeographicalData.src.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BrazilGeographicalData.src.Application.Services.ConfigureServices
{
    public static class ConfigureService
    {
        public static void AddUserContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<UserRepository>();
            builder.Services.AddScoped<IUserFactory, ConcreteUserFactory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(typeof(Program));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }

}
