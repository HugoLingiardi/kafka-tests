using System.IO;
using Kafka.Tests.Core.Adapters;
using Kafka.Tests.Core.Config;
using Kafka.Tests.Core.Logger;
using Kafka.Tests.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kafka.Tests.HostedService
{
    public abstract class HostBuilderFactory
    {

        public static IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = new HostBuilder()
                                .ConfigureAppConfiguration(ConfiguraAppConfiguration)
                                .ConfigureServices(ConfigureServices);

            return hostBuilder;
        }

        private static void ConfiguraAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configBuilder)
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json", optional: true);
            configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            configBuilder.AddEnvironmentVariables();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<ILogger, SerilogLogger>();
            services.AddSingleton<IUniqueIdentifier, HostedServiceUniqueIdentifier>(x =>
            {
                var identifier = new HostedServiceUniqueIdentifier();
                var logger = x.GetRequiredService<ILogger>();

                logger.LogInformation($"Actual id - {identifier.GetUniqueIdentifier()}");

                return identifier;
            });

            services.AddSingleton<KafkaServerConfiguration>(x =>
            {
                var logger = x.GetRequiredService<ILogger>();
                var config = x.GetRequiredService<IConfiguration>();

                var serverUrl = config["KAFKA_DOCKER_SERVER"];

                if (!string.IsNullOrEmpty(serverUrl))
                    logger.LogInformation($"ServerUrl from outside - {serverUrl}");
                else
                    serverUrl = "localhost:9092";

                var kafkaServer = serverUrl;
                var kafkaTopic = "kafka-tests";

                return new KafkaServerConfiguration(kafkaServer, kafkaTopic);
            });

            services.AddScoped<IMessageConsumer, KafkaMessageConsumer>();
            services.AddScoped<IMessageProducer, KafkaMessageProducer>();

            services.AddScoped<MessageProducerAdapter>();
            services.AddScoped<MessageConsumerAdapter>();

            services.AddHostedService<ConsumerHostedService>();
            services.AddHostedService<ProducerHostedService>();
        }

    }
}