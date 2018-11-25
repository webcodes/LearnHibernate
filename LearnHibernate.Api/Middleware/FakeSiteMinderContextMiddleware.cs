namespace LearnHibernate.Api.Middleware
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class FakeSiteMinderContextMiddleware : IMiddleware
    {
        private readonly string standardId;

        public FakeSiteMinderContextMiddleware(IConfiguration config)
        {
            this.standardId = config.GetValue<string>("SSOFakeUser");
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("StandardId", this.standardId) }));
            return next(context);
        }
    }
}
