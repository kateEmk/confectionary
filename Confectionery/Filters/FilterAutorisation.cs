using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Confectionery.Filters
{
    public class FilterAutorisation : Attribute,IAsyncAuthorizationFilter
    {
        public FilterAutorisation()
        {
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { action = "Main", controller = "Home" }
                    )
                );
                return Task.CompletedTask;
            }
            else if (context.HttpContext.User.IsInRole("Admin")) 
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { action = "PanelCompany", controller = "Admin" }
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
