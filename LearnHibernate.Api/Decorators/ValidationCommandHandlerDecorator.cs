using LearnHibernate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHibernate.Api.Decorators
{
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly IValidator validator;
        private readonly ICommandHandler<T> innerHandler;

        public ValidationCommandHandlerDecorator(IValidator validator, ICommandHandler<T> innerHandler)
        {
            this.validator = validator;
            this.innerHandler = innerHandler;
        }

        public void Execute(T command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // validate the supplied command.
            this.validator.ValidateObject(command);

            // forward the (valid) command to the real command handler.
            this.innerHandler.Execute(command);
        }

        public async Task ExecuteAsync(T command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // validate the supplied command.
            this.validator.ValidateObject(command);

            // forward the (valid) command to the real command handler.
            await this.innerHandler.ExecuteAsync(command);
        }
    }
}
