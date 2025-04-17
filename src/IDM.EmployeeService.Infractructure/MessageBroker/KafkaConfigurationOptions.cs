namespace IDM.EmployeeService.Infractructure.MessageBroker
{
    /// <summary>
    /// Модель конфигураций для подключения к kafka
    /// </summary>
    public class KafkaConfigurationOptions
    {
        /// <summary>
        /// Collection of bootstrap service
        /// </summary>
        public string BootstrapServers { get; set; }

        /// <summary>
        /// Topic for create new employee event
        /// </summary>
        public string Topic { get; set; }
    }
}
