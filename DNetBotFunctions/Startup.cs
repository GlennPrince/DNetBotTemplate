using DNetBotFunctions.Clients;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(DNetBotFunctions.Startup))]

namespace DNetBotFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            string cacheConnection = configuration.GetValue<string>("RedisServer");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.GetValue<string>("AzureWebJobsStorage"));

            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheConnection));
            builder.Services.AddSingleton(storageAccount.CreateCloudTableClient(new TableClientConfiguration()));
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddAppsettingsFile(context)
                .AddAppsettingsFile(context, useEnvironment: true)
                .AddEnvironmentVariables();
        }
    }

    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppsettingsFile(
            this IConfigurationBuilder configurationBuilder,
            FunctionsHostBuilderContext context,
            bool useEnvironment = false
        )
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var environmentSection = string.Empty;

            if (useEnvironment)
            {
                environmentSection = $".{context.EnvironmentName}";
            }

            configurationBuilder.AddJsonFile(
                path: Path.Combine(context.ApplicationRootPath, $"appsettings{environmentSection}.json"),
                optional: true,
                reloadOnChange: false);

            return configurationBuilder;
        }
    }
}
