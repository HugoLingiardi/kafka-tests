using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka.Tests.Core.Serializers
{
    public class CustomJsonSerializer<T> : ISerializer<T> where T : class, new()
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var json = JsonConvert.SerializeObject(data);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return bytes;
        }
    }
}