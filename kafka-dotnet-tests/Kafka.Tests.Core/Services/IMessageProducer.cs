using System.Threading.Tasks;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public interface IMessageProducer
    {
        Task SendMessage(Message message);
    }
}