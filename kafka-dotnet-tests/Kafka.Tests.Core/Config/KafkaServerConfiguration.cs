namespace Kafka.Tests.Core.Config
{
    public class KafkaServerConfiguration
    {
        public KafkaServerConfiguration(string serverUrl, string topicName)
        {
            ServerUrl = serverUrl;
            TopicName = topicName;
        }

        public string ServerUrl { get; }
        public string TopicName { get; }
    }
}