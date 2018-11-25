using LearnHibernate.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningHibernate.Proxy.Fake
{
    public class FakeGESProxy : IAuthServiceProxy
    {
        //TODO: Replace with something meaningful. read from env var/file etc?
        public Task<ICollection<string>> GetRolesAsync(string standardId)
        {
            return Task.FromResult((ICollection<string>)new string[]
            {
                "RiskCreatorRole", "RiskApproverRole"
            });
        }

        public Task<bool> HasRoleAsync(string standardId, string role)
        {
            return Task.FromResult(true);
        }
    }
}
