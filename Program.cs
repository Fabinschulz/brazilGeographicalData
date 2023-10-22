using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Infra.Repositories;
using BrazilGeographicalData.src.Persistence.Context;
using BrazilGeographicalData.src.Services;
using BrazilGeographicalData.src.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var builderServices = builder.Services;

builderServices.ConfigureCors();

// Add services to the container.

builderServices.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();

builderServices.AddAuthentication(x =>
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

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    opt.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});

var app = builder.Build();

var options = new RewriteOptions().AddRedirect("^$", "swagger/index.html");
app.UseRewriter(options);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBGE Api v1"));
app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", async (User model) =>
{

    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .Options;

    var userRepo = new UserRepository(new DataContext(options));
    var user = await userRepo.Create(model, model.Password);

    if (user == null)
    {
        throw new NotFoundException("Username or password is incorrect");
    }

    var token = TokenService.GenerateToken(user);
    user.Password = "";

    return Results.Ok(new { user = user, token = token });

});

app.Run();
