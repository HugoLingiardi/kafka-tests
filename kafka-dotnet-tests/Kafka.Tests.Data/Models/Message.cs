using System;

namespace Kafka.Tests.Data.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
