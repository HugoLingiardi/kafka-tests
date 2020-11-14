using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kafka.Tests.Core.Adapters;
using Kafka.Tests.Core.Services;
using Kafka.Tests.Data.Models;
using Moq;
using Xunit;

namespace Kafka.Tests.Tests
{

    [Collection("Basic Tests")]
    public class BasicTests
    {

        private readonly Mock<IMessageProducer> mockMessageProducer;
        private readonly Mock<IMessageConsumer> mockMessageConsumer;

        public BasicTests()
        {
            mockMessageConsumer = new Mock<IMessageConsumer>();
            mockMessageProducer = new Mock<IMessageProducer>();
        }

        [Fact]
        public void MessageConsumerMustReturnValidMessage()
        {
            var cancToken = new CancellationToken();
            mockMessageConsumer.Setup(f => f.ConsumeMessage(cancToken)).Returns(new IdentifiedMessage());

            var consumerAdapter = new MessageConsumerAdapter(mockMessageConsumer.Object);
            var message = consumerAdapter.ConsumeMessage(cancToken);

            message.Should().NotBeNull();
        }

        [Fact]
        public async Task MessageProducerMustNotThrowException()
        {
            var producerAdapter = new MessageProducerAdapter(mockMessageProducer.Object);

            Func<Task> func = async () => await producerAdapter.ProduceMessage(It.IsAny<Message>());

            await func.Should().NotThrowAsync<Exception>();
        }

    }
}