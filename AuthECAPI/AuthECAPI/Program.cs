using AuthECAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services from Add Identity API Endpoints with Entity Framework Stores
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>(); // Replace 'YourDbContext' with your actual DbContext class

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
});

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

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Configure your database provider and connection string here
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevDB"));
});

builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Config. CORS

app.UseCors("AllowAngular");

#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app
    .MapGroup("/api")
    .MapIdentityApi<AppUser>(); // No need to specify user type again here

app.MapPost("/api/signup", async (UserManager<AppUser> userManager, [FromBody] UserRegistrationModel userRegistrationModel) =>
{
    AppUser newUser = new AppUser
    {
        UserName= userRegistrationModel.Email,
        Email = userRegistrationModel.Email,
        FullName = userRegistrationModel.FullName,// You can set this based on your requirements
    };
    var result = await userManager.CreateAsync(newUser, userRegistrationModel.Password);
    if (result.Succeeded)
    {
        // return Results.Ok(result);
        return Results.Ok(new
        {
            succeeded = true,
            message = "User registered successfully"
        });
    }
    else
    {
        //return Results.BadRequest(result);
        return Results.BadRequest(new
        {
            succeeded = false,
            errors = result.Errors
        });

    }
   
});

app.Run();

public class UserRegistrationModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
