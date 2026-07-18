using System.Security.Claims;
using Api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Api.Filters
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var rsultContext=await next();
            if(context.HttpContext.User.Identity?.IsAuthenticated!=true) return;
            var Memberid=context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var DbContext=context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
            await DbContext.Members.Where(x=>x.Id==Memberid).
            ExecuteUpdateAsync(x=>x.SetProperty(x=>x.LastActive,DateTime.UtcNow));

        }
    }
}