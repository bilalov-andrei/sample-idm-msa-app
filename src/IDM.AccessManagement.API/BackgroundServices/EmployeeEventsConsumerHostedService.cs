using Confluent.Kafka;
using IDM.AccessManagement.Application.Commands.RevokeAccess;
using IDM.AccessManagement.Application.Commands.SetupAccess;
using IDM.AccessManagement.Infractructure.Configuration;
using IDM.EmployeeService.IntegrationEvents;
using IDM.EmployeeService.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IDM.AccessManagement.API.HostedServices
{
    public class EmployeeEventsConsumerHostedService : BackgroundService
    {
        private readonly KafkaConfigurationOptions _config;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EmployeeEventsConsumerHostedService> _logger;

        protected string EmployeeNotificationEventTopic { get; }

        public EmployeeEventsConsumerHostedService(
            IOptions<KafkaConfigurationOptions> config,
            IServiceScopeFactory scopeFactory,
            ILogger<EmployeeEventsConsumerHostedService> logger)
        {
            _config = config.Value;
            _scopeFactory = scopeFactory;
            _logger = logger;

            EmployeeNotificationEventTopic = _config.EmployeeNotificationEventTopic;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _config.GroupId,
                BootstrapServers = _config.BootstrapServers,
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe(EmployeeNotificationEventTopic);
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            try
                            {
                                await Task.Yield();
                                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                                var cr = c.Consume(stoppingToken);
                                if (cr != null)
                                {
                                    var message = JsonSerializer.Deserialize<IntegrationEventEnvelope>(cr.Value);
                                    switch (message.EventType)
                                    {
                                        case EmployeeEventType.Hiring:
                                            var hireEmployeeEvent = JsonSerializer.Deserialize<EmployeeHiredIntegrationEvent>(message.IntegrationEventJson);
                                            var setupAccessCommand = new SetupAccessCommand(
                                                hireEmployeeEvent.EmployeeId,
                                                hireEmployeeEvent.EmployeeName,
                                                hireEmployeeEvent.EmployeePosition);
                                            await mediator.Send(setupAccessCommand, stoppingToken);
                                            break;
                                        case EmployeeEventType.Dismissal:
                                            var dissmissalEmployeeEvent = JsonSerializer.Deserialize<EmployeeDismissedIntegrationEvent>(message.IntegrationEventJson);
                                            var revokeAccessCommand = new RevokeAccessCommand(dissmissalEmployeeEvent.EmployeeId);
                                            await mediator.Send(revokeAccessCommand, stoppingToken);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Error while get consume. Message {ex.Message}");
                            }
                        }
                    }
                }
                finally
                {
                    c.Commit();
                    c.Close();
                }
            }
        }
    }
}
