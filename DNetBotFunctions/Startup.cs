using DNetBotFunctions.Clients;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
[assembly: FunctionsStartup(typeof(DNetBotFunctions.Startup))]

namespace DNetBotFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string cacheConnection = Environment.GetEnvironmentVariable("RedisServer").ToString();
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheConnection));
        }
    }
}
