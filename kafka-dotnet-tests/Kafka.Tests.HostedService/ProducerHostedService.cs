using System;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Tests.Core.Adapters;
using Kafka.Tests.Core.Logger;
using Kafka.Tests.Core.Services;
using Kafka.Tests.Data.Models;
using Microsoft.Extensions.Hosting;

namespace Kafka.Tests.HostedService
{
    public class ProducerHostedService : IHostedService, IDisposable
    {
        private Timer timer;

        private readonly ILogger logger;
        private readonly MessageProducerAdapter messageProducerAdapter;

        public ProducerHostedService(ILogger logger, MessageProducerAdapter messageProducerAdapter)
        {
            this.logger = logger;
            this.messageProducerAdapter = messageProducerAdapter;
        }

        private async void ProduceMessage(object state)
        {
            await messageProducerAdapter.ProduceMessage(new Message { Id = Guid.NewGuid().ToString(), Value = "Hello World", Timestamp = DateTime.Now, });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Producer Hosted Service is initialized.");

            timer = new Timer(ProduceMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Producer Hosted Service is stopped.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
            messageProducerAdapter?.Dispose();
        }
    }
}