using System;
using System.Threading.Tasks;
using Kafka.Tests.Core.Services;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Adapters
{
    public class MessageProducerAdapter : IDisposable
    {
        private readonly IMessageProducer messageProducer;

        public MessageProducerAdapter(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        public async Task ProduceMessage(Message message)
        {
            await messageProducer.ProduceMessage(message);
        }

        public void Dispose()
        {
            messageProducer?.Dispose();
        }

    }
}