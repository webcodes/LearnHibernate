namespace LearnHibernate.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using LearnHibernate.Core;
    using LearnHibernate.Proxy.GES;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using Serilog;

    public class GESClaimsMiddleware : IMiddleware
    {
        private readonly GESConfiguration gesConfig;
        private readonly IMemoryCache cache;
        private readonly ILogger logger;
        private readonly IAuthServiceProxy proxy;

        public GESClaimsMiddleware(IAuthServiceProxy proxy, GESConfiguration config, IMemoryCache cache, ILogger logger)
        {
            this.gesConfig = config;
            this.cache = cache;
            this.logger = logger;
            this.proxy = proxy;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.User != null)
            {
                var standardIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ApiConstants.Auth.StandardId);
                if (standardIdClaim != null && !string.IsNullOrEmpty(standardIdClaim.Value))
                {
                    this.logger.Information("The claims store did not have claims for {UserId}. Requesting from GES", standardIdClaim.Value);

                    // TODO: Check if the http request query string has refresh=true
                    // if so, bust the cache and use GES instead?
                    var claimsStoreKey = $"{ApiConstants.Auth.ClaimsStore}:{standardIdClaim.Value}";
                    if (!this.cache.TryGetValue(claimsStoreKey, out ICollection<string> roles))
                    {
                        try
                        {
                            roles = await this.proxy.GetRolesAsync(standardIdClaim.Value);
                            // assuming that caching a user's permission for a work day
                            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1).SetAbsoluteExpiration(TimeSpan.FromHours(8));
                            this.cache.Set(claimsStoreKey, roles, cacheEntryOptions);
                        }
                        catch (Exception ex)
                        {
                            this.logger.Error(ex, "Error encountered while fetching roles for {UserId} from GES", standardIdClaim.Value);
                            throw;
                        }
                    }

                    context.User.AddIdentity(new ClaimsIdentity(roles.Select(r => new Claim(r, "true", ClaimValueTypes.Boolean)), "GES"));
                }
            }

            await next(context);
        }

        private static string GetRolesApiAddress(GESConfiguration config)
        {
            return $"{config.BaseAddress}:{config.Port}/api/namespace/{config.NameSpace}/roles";
        }
    }
}
