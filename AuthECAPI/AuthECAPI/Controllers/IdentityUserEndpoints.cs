using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthECAPI.Controllers
{
    public static class IdentityUserEndpoints
    {
        public static IEndpointRouteBuilder MapIdentityUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/signup", CreateUser);

            app.MapPost("/signin", SignIn);

            return app;
        }

        [AllowAnonymous]
        private static async Task<IResult> CreateUser(UserManager<AppUser> userManager, [FromBody] UserRegistrationModel userRegistrationModel)
        {
            AppUser newUser = new AppUser
            {
                UserName = userRegistrationModel.Email,
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


        }

        [AllowAnonymous]
        private static async Task<IResult> SignIn(UserManager<AppUser> userManager, [FromBody] LoginModel loginModel, IOptions<AppSettings> appSetting)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user != null)
            {
                var isPasswordValid = await userManager.CheckPasswordAsync(user, loginModel.Password);
                if (isPasswordValid)
                {
                    var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.Value.JWTSecret));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                             new Claim("UserID",user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Results.Ok(new { token });
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        succeeded = false,
                        message = "password is incorrect."
                    });
                }
            }
            else
            {
                return Results.BadRequest(new
                {
                    succeeded = false,
                    message = "Username or password is incorrect."
                });
            }


        }
    }
    public class UserRegistrationModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
