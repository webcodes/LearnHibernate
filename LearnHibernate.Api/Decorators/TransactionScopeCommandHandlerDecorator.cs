namespace LearnHibernate.Api.Decorators
{
    using System.Threading.Tasks;
    using LearnHibernate.Core;
    using NHibernate;

    internal class TransactionScopeCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ISession session;
        private readonly ICommandHandler<T> innerHandler;

        public TransactionScopeCommandHandlerDecorator(ISession session, ICommandHandler<T> innerHandler)
        {
            this.session = session;
            this.innerHandler = innerHandler;
        }

        public void Execute(T command)
        {
            using (var transaction = this.session.BeginTransaction())
            {
                this.innerHandler.Execute(command);
                transaction.Commit();
            }
        }

        public async Task ExecuteAsync(T command)
        {
            using (var transaction = this.session.BeginTransaction())
            {
                await this.innerHandler.ExecuteAsync(command);
                await transaction.CommitAsync();
            }
        }
    }
}