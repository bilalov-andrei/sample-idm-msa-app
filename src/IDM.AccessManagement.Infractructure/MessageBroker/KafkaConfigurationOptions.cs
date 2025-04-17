namespace IDM.AccessManagement.Infractructure.Configuration
{
    /// <summary>
    /// Модель конфигураций для подключения к kafka
    /// </summary>
    public class KafkaConfigurationOptions
    {
        /// <summary>
        /// Идентификатор ConsumerGroup
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Топик для потребления событий из employee-service
        /// </summary>
        public string EmployeeNotificationEventTopic { get; set; }
        
        /// <summary>
        /// Адрес сервера кафки 
        /// </summary>
        public string BootstrapServers { get; set; }
    }
}
