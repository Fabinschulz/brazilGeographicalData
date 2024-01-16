using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Behaviors;
using BrazilGeographicalData.src.Domain.Factory;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using BrazilGeographicalData.src.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace BrazilGeographicalData.src.Application.Services.ConfigureServices
{
    public static class ConfigureService
    {
        public static void AddLocationContext(this WebApplicationBuilder builder) 
        {
            builder.Services.AddTransient<LocationRepository>();
            builder.Services.AddScoped<ILocationFactory, ConcreteLocationFactory>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();

        }

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
                    Description = "Uma API para consultar dados geográficos do Brasil.",
                    Contact = new()
                    {
                        Name = "Fabio Lima",
                        Email = "fabio.lima19997@gmail.com"
                    }

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.DocumentFilter<TagDescriptionsDocumentFilter>();
            });
        }

        public static void AddAuthPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole(IdentityData.AdminPolicy));
                opt.AddPolicy("User", policy => policy.RequireRole(IdentityData.UserPolicy));
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(typeof(Program));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add database migration during application startup
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<DataContext>();
                    var logger = serviceProvider.GetRequiredService<ILogger<DataContext>>();

                    context.Database.Migrate();
                    logger.LogInformation("Migração do banco de dados concluída com sucesso.");
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<DataContext>>();
                    logger.LogError(ex, "Erro durante a migração do banco de dados.");
                    throw new Exception("Erro durante a migração do banco de dados.", ex);
                }
            }

        }

        public class TagDescriptionsDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                swaggerDoc.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag { Name = "IBGE", Description = "Brazil Geographical API" },
                    new OpenApiTag { Name = "USER", Description = "User API" }
                };
            }
        }

    }

}
