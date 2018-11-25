namespace LearnHibernate.Api.Middleware
{
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class SiteMinderContextMiddleware : IMiddleware
    {
        //private readonly IClaimsPrincipalProvider principalProvider;

        //public SiteMinderContextMiddleware(IClaimsPrincipalProvider principalProvider)
        //{
        //    this.principalProvider = principalProvider;
        //}

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var standardId = context.Request.Headers["HTTP_UID"];
            var personNumber = context.Request.Headers["HTTP_EMPLOYEENUMBER"];
            var smIdentity = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ApiConstants.Auth.StandardId, standardId),
                    new Claim(ApiConstants.Auth.PersonNumber, personNumber)
                }, "SiteMinder");

            context.User = new ClaimsPrincipal(smIdentity);
            //context.User = this.principalProvider.GetPrincipal(context.Request);
            return next(context);
        }
    }

    //public class SiteMinderBasedPrincipalProvider : IClaimsPrincipalProvider
    //{
    //    public ClaimsPrincipal GetPrincipal(HttpRequest request)
    //    {
    //        var standardId = request.Headers["HTTP_UID"];
    //        var personNumber = request.Headers["HTTP_EMPLOYEENUMBER"];
    //        var smIdentity = new ClaimsIdentity(
    //            new Claim[]
    //            {
    //                new Claim(ApiConstants.Auth.StandardId, standardId),
    //                new Claim(ApiConstants.Auth.PersonNumber, personNumber)
    //            }, "SiteMinder");

    //        return new ClaimsPrincipal(smIdentity);
    //    }
    //}

    //public class FakeBasedPrincipalProvider : IClaimsPrincipalProvider
    //{
    //    private readonly string standardId;

    //    public FakeBasedPrincipalProvider(IConfiguration config)
    //    {
    //        this.standardId = config.GetValue<string>("SSOFakeUser");
    //    }

    //    public ClaimsPrincipal GetPrincipal(HttpRequest request)
    //    {
    //        return new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("StandardId", this.standardId) }));
    //    }
    //}

    //public interface IClaimsPrincipalProvider
    //{
    //    ClaimsPrincipal GetPrincipal(HttpRequest request);
    //}
}
