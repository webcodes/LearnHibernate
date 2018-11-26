namespace LearnHibernate.Api.Tests
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    public class TestServerFixture : IDisposable
    {
        private readonly TestServer testServer;

        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Utility.GetContentRootPath())
                   .UseEnvironment("Development")
                   //.UseSerilog()
                   .UseStartup<Startup>();

            this.testServer = new TestServer(builder);
            this.Client = this.testServer.CreateClient();
        }

        public void Dispose()
        {
            this.Client.Dispose();
            this.testServer.Dispose();
        }
    }
}
