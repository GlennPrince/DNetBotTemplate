using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace DNetBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("Config/hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "BOT_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("Config/appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"Config/appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: "BOT_");
                    configApp.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    configLogging.AddApplicationInsights(hostContext.Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY"));
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureLogging((hostContext, logging) =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddApplicationInsights(hostContext.Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY"));
                    }).UseContentRoot("webroot").UseKestrel();
                })
                .UseConsoleLifetime();
    }
}
