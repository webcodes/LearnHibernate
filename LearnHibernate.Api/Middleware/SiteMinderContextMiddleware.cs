namespace LearnHibernate.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class SiteMinderContextMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var standardId = context.Request.Headers["HTTP_UID"];
            var personNumber = context.Request.Headers["HTTP_EMPLOYEENUMBER"];
            var smIdentity = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("StandardId", standardId),
                    new Claim("PersonNumber", personNumber)
                }, "SiteMinder");

            //TODO: Call GES and find all the claims for this user?
            ClaimsIdentity[] gesIdentities;

            context.User = new ClaimsPrincipal(smIdentity);
            return next(context);
        }
    }
}
