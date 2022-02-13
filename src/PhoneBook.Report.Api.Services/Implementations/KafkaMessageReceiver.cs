using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class KafkaMessageReceiver : IKafkaMessageReceiver
    {
        private readonly IConsumer<string, string> _consumer;
        public KafkaMessageReceiver(IConfiguration config)
        {
            var bootstrapServers = config["Kafka:BootstrapServers"];
            var groupId = config["Kafka:GroupId"];
            var consumerConfig = new ConsumerConfig { BootstrapServers = bootstrapServers, GroupId = groupId };
            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        public void Dispose()
        {
            _consumer.Close(); // Commit offsets and leave the group cleanly.
            _consumer.Dispose();
        }


        public string Receive(CancellationToken cancellationToken)
        {
            try
            {
                var result = _consumer.Consume(cancellationToken);
                return result.Message.Value;
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occured: {e.Error.Reason}");
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Subscribe(string topic)
        {
            if (_consumer == null)
            {
                throw new ArgumentNullException(nameof(_consumer));
            }
            _consumer.Subscribe(topic);
        }
    }
}