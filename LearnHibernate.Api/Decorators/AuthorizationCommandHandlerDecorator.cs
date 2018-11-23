namespace LearnHibernate.Api.Decorators
{
    using System;
    using System.Security;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using LearnHibernate.Core;

    internal class AuthorizationCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> innerHandler;
        private readonly IPrincipal currentUser;

        public AuthorizationCommandHandlerDecorator(
            ICommandHandler<T> innerHandler,
            IPrincipal currentUser)
        {
            this.innerHandler = innerHandler;
            this.currentUser = currentUser;
        }

        public void Execute(T command)
        {
            this.Authorize();
            this.innerHandler.Execute(command);
        }

        public async Task ExecuteAsync(T command)
        {
            this.Authorize();
            await this.innerHandler.ExecuteAsync(command);
        }

        private void Authorize()
        {
            //Check against GES here
            if (false)
            {
                throw new SecurityException();
            }
        }
    }
}