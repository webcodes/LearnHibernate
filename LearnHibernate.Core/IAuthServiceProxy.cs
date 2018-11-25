namespace LearnHibernate.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthServiceProxy
    {
        Task<ICollection<string>> GetRolesAsync(string standardId);

        Task<bool> HasRoleAsync(string standardId, string role);
    }
}
