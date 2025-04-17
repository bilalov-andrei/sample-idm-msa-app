using Confluent.Kafka;
using IDM.EmployeeService.Infractructure.MessageBroker;
using Microsoft.Extensions.Options;

namespace CSharpCourse.EmployeesService.ApplicationServices.MessageBroker
{
    public class ProducerBuilderWrapper : IProducerBuilderWrapper
    {
        public IProducer<string, string> Producer { get; set; }

        public string CreateNewEmployeeTopic { get; set; }

        public string DismissEmployeeTopic { get; set; }


        public ProducerBuilderWrapper(IOptions<KafkaConfigurationOptions> configuration)
        {
            var configValue = configuration.Value ?? throw new ApplicationException("Configuration for kafka server was not specified");
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configValue.BootstrapServers
            };

            Producer = new ProducerBuilder<string, string>(producerConfig).Build();
            CreateNewEmployeeTopic = configValue.Topic;
            DismissEmployeeTopic = configValue.Topic;
        }
    }
}
