using System;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka.Tests.Core.Serializers
{
    public class CustomJsonDeserializer<T> : IDeserializer<T> where T : class, new()
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (!isNull)
            {
                var json = System.Text.Encoding.UTF8.GetString(data.ToArray());
                var result = JsonConvert.DeserializeObject<T>(json);

                return result;
            }

            return null;
        }
    }
}