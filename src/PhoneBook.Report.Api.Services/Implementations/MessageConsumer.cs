using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class MessageConsumer : BackgroundService
    {
        private readonly string _topic;
        private readonly IMessageReceiver _messageReceiver;

        public MessageConsumer(IConfiguration config, IMessageReceiver messageReceiver)
        {
            _topic = config["Messaging:Topic"];
            _messageReceiver = messageReceiver;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private void StartConsumerLoop(CancellationToken cancellationToken)
        {
            _messageReceiver.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _messageReceiver.Receive(cancellationToken);

                    // Handle message...
                    Console.WriteLine($"{cr} received");
                }
                catch (OperationCanceledException)
                {
                    break;
                }               
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }

        public override void Dispose()
        {
            _messageReceiver.Dispose();
            base.Dispose();
        }
    }
}