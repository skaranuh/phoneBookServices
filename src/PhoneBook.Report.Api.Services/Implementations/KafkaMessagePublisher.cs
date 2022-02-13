using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class KafkaMessagePublisher : IKafkaMessagePublisher
    {
        private readonly ProducerConfig _producerConfig;
        public KafkaMessagePublisher(IConfiguration config)
        {
            var configSection = config["Kafka:BootstrapServers"];
            _producerConfig = new ProducerConfig { BootstrapServers = configSection };
        }
        public async Task Publish(string topic, string message)
        {
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                try
                {
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}