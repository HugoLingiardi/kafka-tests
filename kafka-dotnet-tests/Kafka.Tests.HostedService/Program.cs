using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Kafka.Tests.HostedService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = HostBuilderFactory.CreateHostBuilder();

            await hostBuilder.RunConsoleAsync();
        }
    }
}
