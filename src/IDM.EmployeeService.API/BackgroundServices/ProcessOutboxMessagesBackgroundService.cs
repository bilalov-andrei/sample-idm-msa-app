using Confluent.Kafka;
using CSharpCourse.EmployeesService.ApplicationServices.MessageBroker;
using IDM.Common.Domain;
using IDM.EmployeeService.Infractructure.Processing.Outbox;
using IDM.EmployeeService.IntegrationEvents;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IDM.EmployeeService.API.BackgroundServices;

public class ProcessOutboxMessagesBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<OutboxConfigurationOptions> _outboxOptions;
    private readonly ILogger<ProcessOutboxMessagesBackgroundService> _logger;
    private readonly IProducerBuilderWrapper _producerBuilderWrapper;

    public ProcessOutboxMessagesBackgroundService(
        IServiceProvider serviceProvider,
        IOptions<OutboxConfigurationOptions> outboxOptions,
        ILogger<ProcessOutboxMessagesBackgroundService> logger,
        IProducerBuilderWrapper producerBuilderWrapper)
    {
        _serviceProvider = serviceProvider;
        _outboxOptions = outboxOptions;
        _logger = logger;
        _producerBuilderWrapper = producerBuilderWrapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var outboxMessageRepository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                int processedMessages;
                do
                {
                    processedMessages = await ProcessMessagesAsync(outboxMessageRepository, unitOfWork, stoppingToken);
                } while (processedMessages > 0);

                await Task.Delay(TimeSpan.FromMilliseconds(_outboxOptions.Value.NoMessagesDelay), stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something wrong");
            }
        }
    }

    private async Task<int> ProcessMessagesAsync(IOutboxMessageRepository outboxMessageRepository, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        await unitOfWork.StartTransaction(cancellationToken);

        var outboxMessages = await outboxMessageRepository.GetForProcessingAsync(_outboxOptions.Value, cancellationToken);

        if (!outboxMessages.Any()) return 0;

        await ProcessOutboxMessagesAsync(outboxMessages, cancellationToken);

        await outboxMessageRepository.UpdateAsync(outboxMessages, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return outboxMessages.Length;
    }

    private async Task ProcessOutboxMessagesAsync(OutboxMessage[] outboxMessages, CancellationToken cancellationToken)
    {
        foreach (var message in outboxMessages)
        {
            try
            {
                await _producerBuilderWrapper.Producer.ProduceAsync(_producerBuilderWrapper.CreateNewEmployeeTopic,
                    new Message<string, string>()
                    {
                        Key = message.Key,
                        Value = JsonSerializer.Serialize(new IntegrationEventEnvelope()
                        {
                            EventType = message.EventType,
                            IntegrationEventJson = message.Payload
                        })
                    }, cancellationToken);

                message.ProcessedAt = DateTime.UtcNow;
            }
            catch (Exception e)
            {
                if (message.RetryCount >= 3)  //max retry count
                {
                    message.Failed = true;
                }
                else
                {
                    message.RetryCount++;
                    message.RetryAfter = DateTime.UtcNow.AddSeconds(1); // some strategy
                }
            }
        }
    }
}
