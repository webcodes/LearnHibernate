namespace LearnHibernate.Api.Decorators
{
    using System.Threading.Tasks;
    using LearnHibernate.Core;
    using Serilog;

    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> innerHandler;
        private readonly ILogger logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<T> innerHandler, ILogger logger)
        {
            this.innerHandler = innerHandler;
            this.logger = logger;
        }

        public void Execute(T command)
        {
            var commandHandlerName = this.innerHandler.GetType().FullName;
            this.logger.Information("Invoking {CommandHandler} for {@Command}", commandHandlerName, command);
            this.innerHandler.Execute(command);
            this.logger.Information("Returning from {CommandHandler}", commandHandlerName);
        }

        public async Task ExecuteAsync(T command)
        {
            var commandHandlerName = this.innerHandler.GetType().FullName;
            this.logger.Information("Invoking {CommandHandler} for {@Command}", commandHandlerName, command);
            await this.innerHandler.ExecuteAsync(command);
            this.logger.Information("Returning from {CommandHandler}", commandHandlerName);
        }
    }
}