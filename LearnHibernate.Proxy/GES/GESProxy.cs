namespace LearnHibernate.Proxy.GES
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using LearnHibernate.Core;
    using Newtonsoft.Json;

    public class GESProxy : IAuthServiceProxy
    {
        private readonly HttpClient httpClient;
        private readonly GESConfiguration gesConfig;

        public GESProxy(HttpClient httpClient, GESConfiguration config)
        {
            this.httpClient = httpClient;
            this.gesConfig = config;
        }

        public async Task<ICollection<string>> GetRolesAsync(string standardId)
        {
            var response = await this.httpClient.GetAsync($"api/{this.gesConfig.NameSpace}/roles/{standardId}");
            if (response.IsSuccessStatusCode)
            {
                var rolesJson = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<ICollection<GESRole>>(rolesJson);
                return roles.Select(r => r.Name).ToList();
            }

            return Enumerable.Empty<string>().ToList();
        }

        public async Task<bool> HasRoleAsync(string standardId, string role)
        {
            var response = await this.httpClient.GetAsync($"api/{this.gesConfig.NameSpace}/isinrole/{standardId}/{role}");
            if (response.IsSuccessStatusCode)
            {
                var boolJson = await response.Content.ReadAsStringAsync();
                var hasRole = JsonConvert.DeserializeObject<bool>(boolJson);
                return hasRole;
            }

            return false;
        }

        private class GESRole
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
