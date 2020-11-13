using System;
using Kafka.Tests.Core.Services;

namespace Kafka.Tests.HostedService
{
    public class HostedServiceUniqueIdentifier : IUniqueIdentifier
    {
        private readonly string uniqueIdentifier;

        public HostedServiceUniqueIdentifier()
        {
            this.uniqueIdentifier = Guid.NewGuid().ToString();
        }
        
        public string GetUniqueIdentifier() => uniqueIdentifier;

    }

}