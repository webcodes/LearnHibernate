namespace LearnHibernate.Api.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ServiceStack;
    using Xunit;

    public class ValueControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture fixture;

        public ValueControllerTests(TestServerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetAllValues_ReturnsTwoStrings()
        {
            var response = await this.fixture.Client.GetAsync("api/values");
            response.EnsureSuccessStatusCode();

            var valuesJson = await response.Content.ReadAsStringAsync();
            var values = valuesJson.FromJson<IEnumerable<string>>();
            Assert.Equal(2, values.Count());
        }
    }
}
