using IDM.EmployeeService.IntegrationEvents.Events;

namespace IDM.EmployeeService.IntegrationEvents
{
    /// <summary>
    /// Содержит объект события и его метаданные
    /// </summary>
    public class IntegrationEventEnvelope
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public EmployeeEventType EventType { get; set; }

        public string IntegrationEventJson { get; set; }
    }
}
