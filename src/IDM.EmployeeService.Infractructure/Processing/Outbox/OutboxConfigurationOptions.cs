namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public class OutboxConfigurationOptions
    {
        public int NoMessagesDelay { get; set; } = 1000;
        public int BatchSize { get; set; } = 10;
    }
}
