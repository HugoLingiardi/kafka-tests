namespace Kafka.Tests.Data.Models
{
    public class IdentifiedMessage
    {
        public string ParentId { get; set; }
        public Message Message { get; set; }
    }
}