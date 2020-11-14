using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Kafka.Tests.HostedService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Vou iniciar o HostService.");

            try
            {
                var hostBuilder = HostBuilderFactory.CreateHostBuilder();

                await hostBuilder.RunConsoleAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar programa, exceção: {ex}");
            }
        }
    }
}
