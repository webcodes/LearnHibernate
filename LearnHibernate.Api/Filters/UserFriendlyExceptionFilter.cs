namespace LearnHibernate.Api.Filters
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;

    public class UserFriendlyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Log.Logger.Error(context.Exception, "Something went wrong");
            if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Result = new JsonResult(new { Message = "Your request could not be completed. Please reach out to the team." });
            base.OnException(context);
        }

        //public override Task OnExceptionAsync(ExceptionContext context)
        //{
        //    Log.Logger.Error(context.Exception, "Something went wrong");
        //    if (context.Exception is UnauthorizedAccessException)
        //    {
        //        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //    }
        //    else
        //    {
        //        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    }

        //    context.Result = new JsonResult(new { Message = "Your request could not be completed. Please reach out to the team." });
        //    return base.OnExceptionAsync(context);
        //}
    }
}
