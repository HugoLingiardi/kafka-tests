using System;
using System.Threading;
using Kafka.Tests.Data.Models;

namespace Kafka.Tests.Core.Services
{
    public interface IMessageConsumer : IDisposable
    {
        IdentifiedMessage ConsumeMessage(CancellationToken cancellationToken);
    }
}