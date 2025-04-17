using IDM.EmployeeService.IntegrationEvents.Events;

namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public class OutboxMessage
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public EmployeeEventType EventType { get; set; }
        public string Payload { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int RetryCount { get; set; }
        public bool Failed { get; set; }
        public DateTime? RetryAfter { get; set; }

        public OutboxMessage()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
