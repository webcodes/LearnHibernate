namespace LearnHibernate.Api
{
    using System;
    using System.Net.Http;
    using LearnHibernate.Core;
    using LearnHibernate.Proxy;
    using LearnHibernate.Proxy.GES;
    using LearningHibernate.Proxy.Fake;
    using Microsoft.Extensions.DependencyInjection;
    using Polly;
    using Polly.Extensions.Http;
    using Polly.Retry;
    using Polly.Timeout;

    public static class ProxyConfigurator
    {
        private static readonly RetryPolicy<HttpResponseMessage> RetryPolicy;
        private static readonly TimeoutPolicy<HttpResponseMessage> TimeoutPolicy;
        private static readonly IAsyncPolicy<HttpResponseMessage> NoOpPolicy;

        static ProxyConfigurator()
        {
            //TODO: Change this to use a policy registry?
            RetryPolicy = HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .RetryAsync(3);
             TimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
             NoOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
        }

        public static IServiceCollection AddGESProxy(this IServiceCollection services, GESConfiguration gesConfig)
        {
            services.AddHttpClient<IAuthServiceProxy, GESProxy>(config =>
            {
                config.BaseAddress = new Uri($"{gesConfig.BaseAddress}:{gesConfig.Port}");
            })
            .AddHttpMessageHandler<LoggingHTTPHandler>()
            .AddPolicyHandler(TimeoutPolicy)

            // assuming that a GET request is truely idempotent on GES
            .AddPolicyHandler(req => req.Method == HttpMethod.Get ? RetryPolicy : NoOpPolicy);
            return services;
        }

        public static IServiceCollection AddFakeGESProxy(this IServiceCollection services)
        {
            services.AddTransient<IAuthServiceProxy, FakeGESProxy>();
            return services;
        }

        //TODO: Add Jolt
    }
}
