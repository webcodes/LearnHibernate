namespace LearnHibernate.Core.Commands.Employee
{
    using System.Threading.Tasks;
    using LearnHibernate.Pocos.Command.Employee;

    public interface IEmployeeStore
    {
        Task<object> AddEmployeeAsync(AddEmployeeCommand command);
    }
}