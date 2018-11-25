namespace LearnHibernate.Proxy
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingHTTPHandler : DelegatingHandler
    {
        private readonly ILogger logger;

        public LoggingHTTPHandler(ILogger logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
        CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            this.logger.Information("Starting request");

            var response = await base.SendAsync(request, cancellationToken);

            this.logger.Information($"Finished request in {sw.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
