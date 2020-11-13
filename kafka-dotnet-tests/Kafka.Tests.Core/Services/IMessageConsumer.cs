using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public interface IMessageConsumer
    {
        Message ConsumeMessage();
    }
}