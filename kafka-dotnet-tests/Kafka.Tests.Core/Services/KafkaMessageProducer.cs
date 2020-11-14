using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.Tests.Core.Config;
using Kafka.Tests.Data.Models;
using Kafka.Tests.Core.Serializers;

namespace Kafka.Tests.Core.Services
{

    public class KafkaMessageProducer : IMessageProducer, IDisposable
    {

        private readonly IProducer<Null, IdentifiedMessage> producer;

        private readonly KafkaServerConfiguration configuration;
        private readonly IUniqueIdentifier uniqueIdentifier;

        public KafkaMessageProducer(KafkaServerConfiguration configuration, IUniqueIdentifier uniqueIdentifier)
        {
            this.configuration = configuration;
            this.uniqueIdentifier = uniqueIdentifier;

            producer = new ProducerBuilder<Null, IdentifiedMessage>(new ProducerConfig { BootstrapServers = configuration.ServerUrl }).SetValueSerializer(new CustomJsonSerializer<IdentifiedMessage>()).Build();
        }

        public void Dispose()
        {
            producer?.Dispose();
        }

        public async Task ProduceMessage(Message message)
        {
            var id = uniqueIdentifier.GetUniqueIdentifier();

            var IdentifiedMessage = new IdentifiedMessage
            {
                ParentId = id,
                Message = message,
            };

            await producer.ProduceAsync(configuration.TopicName, new Message<Null, IdentifiedMessage> { Value = IdentifiedMessage });
        }
    }
}