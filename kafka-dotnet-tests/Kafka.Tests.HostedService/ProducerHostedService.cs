using System;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Tests.Core.Services;
using Kafka.Tests.Data.Models;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kafka.Tests.HostedService
{
    public class ProducerHostedService : IHostedService, IDisposable
    {
        private Timer timer;

        private readonly ILogger logger;
        private readonly IMessageProducer messageProducer;

        public ProducerHostedService(ILogger logger, IMessageProducer messageProducer)
        {
            this.logger = logger;
            this.messageProducer = messageProducer;
        }

        private void ProduceMessage(object state)
        {
            messageProducer.SendMessage(new Message { Id = Guid.NewGuid().ToString(), Value = "Hello World", Timestamp = DateTime.Now, });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.Information("Producer Hosted Service is initializing.");

            timer = new Timer(ProduceMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Information("Producer Hosted Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}