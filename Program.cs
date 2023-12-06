using BrazilGeographicalData.src.Application.Services.ConfigureServices;
using BrazilGeographicalData.src.Application.Services.Extensions;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var builderServices = builder.Services;

builderServices.ConfigureCorsPolicy();
builderServices.AddControllers();
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();
builder.AddUserContext();
builder.AddDatabase();
builder.AddSwaggerDoc();
builder.AddAuthPolicy();
builder.AddAuthJwt();
builderServices.ConfigureServices();

var app = builder.Build();

var options = new RewriteOptions().AddRedirect("^$", "swagger/index.html");
app.UseRewriter(options);

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBGE Api v1"));
app.UseErrorHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();

app.Run();
