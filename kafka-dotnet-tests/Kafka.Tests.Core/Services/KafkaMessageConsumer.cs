using System.Threading.Tasks;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public class KafkaMessageConsumer : IMessageConsumer
    {
        private readonly IUniqueIdentifier uniqueIdentifier;

        public KafkaMessageConsumer(IUniqueIdentifier uniqueIdentifier)
        {
            this.uniqueIdentifier = uniqueIdentifier;
        }

        Message IMessageConsumer.ConsumeMessage()
        {
            throw new System.NotImplementedException();
        }
    }
}