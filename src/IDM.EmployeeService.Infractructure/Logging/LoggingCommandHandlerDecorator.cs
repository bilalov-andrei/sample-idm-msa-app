using IDM.Common.Application.Commands;
using Microsoft.Extensions.Logging;

namespace IDM.EmployeeService.Infractructure.Logging
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ILogger _logger;

        private readonly ICommandHandler<T> _decorated;

        public LoggingCommandHandlerDecorator(
            ILogger logger,
            ICommandHandler<T> decorated)
        {
            _logger = logger;
            _decorated = decorated;
        }
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            try
            {
                this._logger.LogInformation("Executing command {Command}", command.GetType().Name);

                await _decorated.Handle(command, cancellationToken);

                this._logger.LogInformation("Command {Command} processed successful", command.GetType().Name);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, "Command {Command} processing failed", command.GetType().Name);
                throw;
            }
        }
    }
}
