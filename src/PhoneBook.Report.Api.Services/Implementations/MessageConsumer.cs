using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class MessageConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => await StartConsumerLoop(stoppingToken));
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var topic = config["Messaging:Topic"];
                var messageReceiver = scope.ServiceProvider.GetRequiredService<IMessageReceiver>();
                var reportGenerator = scope.ServiceProvider.GetRequiredService<IReportGenerator>();

                messageReceiver.Subscribe(topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var message = messageReceiver.Receive(cancellationToken);
                        var reportRequestId = Guid.Parse(message);
                        Console.WriteLine($"Report request : {reportRequestId} received");
                        await reportGenerator.GenerateReport(reportRequestId);
                        Console.WriteLine($"Report request : {reportRequestId} completed");
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine($"Report request failed: {ex.ToString()}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Report request failed: Unexpected error: {e}");
                    }
                }

            }
        }
    }
}