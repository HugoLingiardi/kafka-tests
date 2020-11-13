using System.IO;
using Kafka.Tests.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
            services.AddSingleton<ILogger>(i => new LoggerConfiguration().WriteTo.Console().CreateLogger());
            services.AddSingleton<IUniqueIdentifier, HostedServiceUniqueIdentifier>();

            services.AddScoped<IMessageConsumer, KafkaMessageConsumer>();
            services.AddScoped<IMessageProducer, KafkaMessageProducer>();

            services.AddHostedService<ConsumerHostedService>();
            services.AddHostedService<ProducerHostedService>();
        }

    }
}