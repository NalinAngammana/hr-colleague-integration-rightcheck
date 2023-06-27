using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Consumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
               
                var now = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
                Console.WriteLine($"{now}: Service Starting.");

                var hostBuilder = await CreateHostBuilder(args, config);
                var hostBuilderBuild = hostBuilder.Build();
                await hostBuilderBuild.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        internal static Task<IHostBuilder> CreateHostBuilder(string[] args, IConfigurationRoot configRoot) =>
            Task.FromResult(Host.CreateDefaultBuilder(args).UseWindowsService()
                .ConfigureServices((_, services) =>
                {
                    services.ConfigureServices(configRoot);
                }));
    }
}
