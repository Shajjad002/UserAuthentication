using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthECAPI.Controllers
{
    public static class AccountEndPoints
    {
        public static IEndpointRouteBuilder MapAccountEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", GetUserProfile);
            return app;
        }

        [Authorize]
        private static async Task<IResult> GetUserProfile(ClaimsPrincipal user, UserManager<AppUser> userManager)
        {
            string userId = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            var userDetails = await userManager.FindByIdAsync(userId);
            return Results.Ok(new
            {
                FullName= userDetails?.FullName,
                Email= userDetails?.Email,
                //UserName = userDetails?.UserName
            });
        }
    }
}
