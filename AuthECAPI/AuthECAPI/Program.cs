using AuthECAPI.Controllers;
using AuthECAPI.Extensions;
using AuthECAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Add CORS Registration HERE (Before builder.Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration)
                .AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration)
                .AddSwaggerExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwaggerExplorer()
   .AddIdentityAuthMiddlewares();

// UseCors stays here, after builder.Build()
app.UseCors("AllowAngular");

//app.UseAuthentication();
//app.UseAuthorization();


app.UseHttpsRedirection();
app.MapControllers();

app .MapGroup("/api")
    .MapIdentityApi<AppUser>(); // No need to specify user type again here

app .MapGroup("/api")
    .MapIdentityUserEndpoints()
    .MapAccountEndPoints();


app.Run();

