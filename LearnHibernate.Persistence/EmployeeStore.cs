namespace LearnHibernate.Persistence
{
    using System;
    using System.Threading.Tasks;
    using LearnHibernate.Core.Commands.Employee;
    using LearnHibernate.Entity;
    using LearnHibernate.Pocos.Command.Employee;
    using NHibernate;

    public class EmployeeStore : IEmployeeStore
    {
        private readonly ISession session;

        public EmployeeStore(ISession session)
        {
            this.session = session;
        }

        public async Task<object> AddEmployeeAsync(AddEmployeeCommand command)
        {
            Employee employee = this.GetEmployeeFromCommand(command);
            return await this.session.SaveAsync(employee);
        }

        private Employee GetEmployeeFromCommand(AddEmployeeCommand command)
        {
            var birthDate = DateTime.UtcNow.AddYears(-30).Date;
            var startDate = DateTime.UtcNow.AddYears(-1).Date;
            return new Employee
            {
                FirstName = "Awesome",
                LastName = "Developer",
                EmailAddress = "awesome@developer.com",
                IsAdmin = true,
                PasswordHash = "secret",
                StartDate = startDate,
                BirthDate = birthDate
            };
        }
    }
}
