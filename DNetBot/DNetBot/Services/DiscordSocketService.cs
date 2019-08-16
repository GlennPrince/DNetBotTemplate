using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IApplicationLifetime _appLifetime;
        private readonly IConfiguration _config;

        private DiscordShardedClient discordClient;
        private string botToken;

        public DiscordSocketService(
            ILogger<DiscordSocketService> logger,
            IApplicationLifetime appLifetime,
            IConfiguration config)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _config = config;
            botToken = _config["DISCORD_BOT_TOKEN"];
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            Discord.LogSeverity logLevel = LogSeverity.Info;

            if (_config["LOGLEVEL"] == "DEBUG")
                logLevel = LogSeverity.Debug;
            else if (_config["LOGLEVEL"] == "WARNING")
                logLevel = LogSeverity.Warning;
            else if (_config["LOGLEVEL"] == "ERROR")
                logLevel = LogSeverity.Error;

            // Setup the Discord Client Configuration
            discordClient = new DiscordShardedClient(new DiscordSocketConfig
            {
                LogLevel = logLevel
            });

            ConfigureEventHandlers();

            discordClient.LoginAsync(Discord.TokenType.Bot, botToken).Wait();
            discordClient.StartAsync().Wait();
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
            discordClient.LogoutAsync();
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        private void ConfigureEventHandlers()
        {
            // Set the Discord Client logging to use the system logger.
            discordClient.Log += async l =>
            {
                var log = Formatter.GenerateLog(l);
                _logger.Log(log.level, log.message);
            };

            // General message handling event
            discordClient.MessageReceived += async m => await RecieveMessage(m);

            // Bot specific events
            discordClient.CurrentUserUpdated += async (o, n) => await BotUpdated(o, n);
            discordClient.ShardReady += async r => await ShardReady(r);
            discordClient.LoggedIn += async () => await LoggedIn();
        }

        private Task ShardReady(DiscordSocketClient client)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Shard Ready - Shard ID:" + client.ShardId);

            return Task.CompletedTask;
        }

        private Task BotUpdated(SocketSelfUser oldBot, SocketSelfUser newBot)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Bot Updated - " + newBot.ToString());

            return Task.CompletedTask;
        }

        private Task LoggedIn()
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Client Logged In");

            return Task.CompletedTask;
        }
    }
}
