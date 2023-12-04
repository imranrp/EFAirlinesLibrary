using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AirlinesMvcApp.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            RouteData data = context.RouteData;
            var controller = data.Values["controller"];
            var action = data.Values["action"];
            Debug.WriteLine($"The {action} action in {controller} controller executing at {DateTime.Now.ToLongTimeString()}");
        }
    }
}
