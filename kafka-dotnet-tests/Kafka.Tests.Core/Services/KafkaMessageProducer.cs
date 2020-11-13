using System.Threading.Tasks;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public class KafkaMessageProducer :IMessageProducer
    {
        private readonly IUniqueIdentifier uniqueIdentifier;

        public KafkaMessageProducer(IUniqueIdentifier uniqueIdentifier)
        {
            this.uniqueIdentifier = uniqueIdentifier;
        }

        public Task SendMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}