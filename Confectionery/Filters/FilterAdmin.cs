using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.Filters
{
    public class FilterAdmin : Attribute, IAsyncAuthorizationFilter
        {
        public FilterAdmin()
        {
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated || !context.HttpContext.User.IsInRole("Admin"))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { action = "Main", controller = "Home" }
                    )
                );
                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
