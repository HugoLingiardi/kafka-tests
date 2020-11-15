using System;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Tests.Core.Adapters;
using Kafka.Tests.Core.Logger;
using Microsoft.Extensions.Hosting;

namespace Kafka.Tests.HostedService
{
    public class ConsumerHostedService : IHostedService, IDisposable
    {
        private bool running;

        private readonly ILogger logger;
        private readonly MessageConsumerAdapter messageConsumerAdapter;

        public ConsumerHostedService(ILogger logger, MessageConsumerAdapter messageConsumerAdapter)
        {
            this.logger = logger;
            this.messageConsumerAdapter = messageConsumerAdapter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Consumer Hosted Service is initiliazed.");

            running = true;

            Task.Run(() => ConsumeMessages(cancellationToken));

            return Task.CompletedTask;
        }

        public void ConsumeMessages(CancellationToken cancellationToken)
        {
            while (running)
            {
                var identifiedMessage = messageConsumerAdapter.ConsumeMessage(cancellationToken);

                if (identifiedMessage != null)
                    logger.Log(LogType.Information, $"Message received - parent id : {identifiedMessage.ParentId} id: {identifiedMessage.Message.Id} value: {identifiedMessage.Message.Value} timestamp: {identifiedMessage.Message.Timestamp}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Consumer Hosted Service is stopped.");

            running = false;

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            messageConsumerAdapter?.Dispose();
        }
    }
}