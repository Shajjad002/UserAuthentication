using AuthECAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthECAPI.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityHandlersAndStores(this IServiceCollection services)
        {
            // services.AddTransient<IUserClaimsPrincipalFactory<AppUser>, UserClaimsPrincipalFactory<AppUser, IdentityRole>>();
            services.AddIdentityApiEndpoints<AppUser>()
            .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }

        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }

        //Auth = Authentication + Authorization
        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(y =>
            {
                y.SaveToken = false;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AppSettings:JWTSecret"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            services.AddAuthorization(optins=>
            {
                optins.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
                    //optins.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                    //optins.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            });
            return services;
        }

        public static WebApplication AddIdentityAuthMiddlewares(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();
        }
    }
}
