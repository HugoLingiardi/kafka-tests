using System;
using System.Threading.Tasks;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public interface IMessageProducer : IDisposable
    {
        Task ProduceMessage(Message message);
    }
}