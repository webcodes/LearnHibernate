namespace LearnHibernate.Core.Commands.Employee
{
    using System.Threading.Tasks;
    using LearnHibernate.Pocos.Command.Employee;

    public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand>
    {
        //TODO: Not convinced IEmployeeStore is any better than service/repo?
        //But if we have session dependency in core layer, then we have nH dependency
        //if we move commandHandlers to persistence, then the command handler cannot be a true
        // business layer. Cannot handle amps, notification, jolt etc?
        //Can we have a UoW created. would that solve the problem?
        private readonly IEmployeeStore dataStore;

        public AddEmployeeCommandHandler(IEmployeeStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public void Execute(AddEmployeeCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task ExecuteAsync(AddEmployeeCommand command)
        {
            await this.dataStore.AddEmployeeAsync(command);
            //notify through email
            //publish to AMPS?
        }
    }
}
