using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data.Repository.General;
using DatingApp.API.Data.Repository.Users;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // ActionExecutingContext do something when the action executed
            // ActionExecutionDelegate do something after the action been executed
            var resultContext = await next();
            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var datingRepo = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();
            var userRepo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await userRepo.GetUser(userId);
            user.LastActive = DateTime.Now;
            await datingRepo.SaveChangesAsync();
        }
    }
}