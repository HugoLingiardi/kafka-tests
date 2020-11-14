using System;
using System.Threading;
using Kafka.Tests.Core.Services;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Adapters
{

    public class MessageConsumerAdapter : IDisposable
    {
        private readonly IMessageConsumer messageConsumer;

        public MessageConsumerAdapter(IMessageConsumer messageConsumer)
        {
            this.messageConsumer = messageConsumer;
        }

        public IdentifiedMessage ConsumeMessage(CancellationToken cancellationToken)
        {
            return messageConsumer.ConsumeMessage(cancellationToken);
        }

        public void Dispose()
        {
            messageConsumer?.Dispose();
        }
    }
}