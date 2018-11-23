namespace LearnHibernate.Api.Filters
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;

    public class LoggingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            Log.Logger.Information("Request to {ApiRoute} @ {Controller}/{Action} completed", actionDescriptor.AttributeRouteInfo.Template, actionDescriptor.ControllerName, actionDescriptor.ActionName);
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            Log.Logger.Information("Request to {Controller}/{Action} invoked with parameters {RouteValue}", actionDescriptor.ControllerName, actionDescriptor.ActionName, actionDescriptor.RouteValues);
            base.OnActionExecuting(context);
        }
    }
}
