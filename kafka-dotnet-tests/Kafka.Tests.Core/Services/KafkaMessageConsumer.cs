using System;
using System.Threading;
using Confluent.Kafka;
using Kafka.Tests.Core.Config;
using Kafka.Tests.Data.Models;
using Kafka.Tests.Core.Serializers;

namespace Kafka.Tests.Core.Services
{
    public class KafkaMessageConsumer : IMessageConsumer, IDisposable
    {
        private readonly IConsumer<Ignore, IdentifiedMessage> consumer;

        private readonly KafkaServerConfiguration configuration;
        private readonly IUniqueIdentifier uniqueIdentifier;

        public KafkaMessageConsumer(KafkaServerConfiguration configuration, IUniqueIdentifier uniqueIdentifier)
        {
            this.configuration = configuration;
            this.uniqueIdentifier = uniqueIdentifier;

            consumer = new ConsumerBuilder<Ignore, IdentifiedMessage>(new ConsumerConfig
            {
                BootstrapServers = configuration.ServerUrl,
                GroupId = $"{configuration.TopicName}-group-{new Random().Next(15)}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            }).SetValueDeserializer(new CustomJsonDeserializer<IdentifiedMessage>()).Build();

            consumer.Subscribe(configuration.TopicName);
        }

        public void Dispose()
        {
            consumer?.Dispose();
        }

        IdentifiedMessage IMessageConsumer.ConsumeMessage(CancellationToken cancellationToken)
        {
            var processId = uniqueIdentifier.GetUniqueIdentifier();

            IdentifiedMessage identifiedMessage = null;
            do
            {

                try
                {
                    var result = consumer.Consume(cancellationToken);

                    identifiedMessage = result.Message.Value;
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();

                    throw;
                }

            } while (identifiedMessage.ParentId == processId);

            return identifiedMessage;
        }
    }
}