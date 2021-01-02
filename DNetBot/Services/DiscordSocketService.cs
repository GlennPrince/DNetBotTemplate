using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConfiguration _config;

        private DiscordShardedClient discordClient;
        private string botToken;

        private string eventGridDomainEndpoint;
        private string eventGridDomainAccessKey;
        private string eventGridDomainHostname;
        private static EventGridClient eventGridClient;
        private TopicCredentials eventGridCredentials;

        public DiscordSocketService(
            ILogger<DiscordSocketService> logger,
            IHostApplicationLifetime appLifetime,
            IConfiguration config)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _config = config;
            botToken = _config["DISCORD_BOT_TOKEN"];
            eventGridDomainEndpoint = _config["EventGridDomainEndPoint"];
            eventGridDomainAccessKey = _config["eventGridDomainAccessKey"];
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

            ConfigureEventGrid();

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

        private void ConfigureEventGrid()
        {
            _logger.LogInformation("Configuring EventGrid | Endpoint: " + eventGridDomainEndpoint);
            eventGridDomainHostname = new Uri(eventGridDomainEndpoint).Host;
            _logger.LogInformation("Configuring EventGrid | Domain Hostname: " + eventGridDomainHostname);
            eventGridCredentials = new TopicCredentials(eventGridDomainAccessKey);
            eventGridClient = new EventGridClient(eventGridCredentials);
            _logger.LogInformation("Configuring EventGrid | EventGrid Client Version: " + eventGridClient.ApiVersion);
        }

        private void ConfigureEventHandlers()
        {
            // Set the Discord Client logging to use the system logger.
            discordClient.Log += async l =>
            {
                var log = Formatter.GenerateLog(l);
                _logger.Log(log.level, log.message);
            };

            // Events that occur when a bot is added or removed from a server
            discordClient.JoinedGuild += async g => await GuildJoin(g);
            discordClient.LeftGuild += async g => await GuildJoin(g);

            // Events that occur when a server information changes
            discordClient.GuildAvailable += async g => await GuildAvailable(g);
            discordClient.GuildUpdated += async (o, n) => await GuildUpdate(o, n);
            discordClient.GuildUnavailable += async g => await GuildUnAvailable(g);

            // Events that occur on a server when channel information changes
            discordClient.ChannelCreated += async c => await ChannelCreated(c);
            discordClient.ChannelUpdated += async (o, n) => await ChannelUpdated(o, n);
            discordClient.ChannelDestroyed += async c => await ChannelDeleted(c);
            discordClient.RecipientAdded += async r => await ChannelJoined(r);
            discordClient.RecipientRemoved += async r => await ChannelLeft(r);

            // Events that occur on a server when role information changes
            discordClient.RoleCreated += async r => await RoleCreated(r);
            discordClient.RoleUpdated += async (o, n) => await RoleUpdated(o, n);
            discordClient.RoleDeleted += async r => await RoleDeleted(r);

            // Events that occur when user information on a server changes
            discordClient.UserBanned += async (u, g) => await UserBan(u, g);
            discordClient.UserUnbanned += async (u, g) => await UserUnban(u, g);

            // User based events
            discordClient.UserJoined += async u => await UserJoined(u);
            discordClient.UserLeft += async u => await UserLeft(u);
            discordClient.UserUpdated += async (o, n) => await UserUpdate(o, n);
            discordClient.GuildMemberUpdated += async (o, n) => await MemberUpdate(o, n);
            discordClient.RecipientAdded += async u => await PrivateGroupAdd(u);
            discordClient.RecipientRemoved += async u => await PrivateGroupRemove(u);

            // General message handling event
            discordClient.MessageReceived += async m => await ReceiveMessage(m);
            discordClient.MessageDeleted += async (m, c) => await DeletedMessage(m.Id, c);
            discordClient.MessageUpdated += async (m, u, c) => await UpdatedMessage(m.Id, u, c);

            // Reactions Handling
            discordClient.ReactionAdded += async (m, c, r) => await ReactionAdded(m, c, r);
            discordClient.ReactionRemoved += async (m, c, r) => await ReactionRemoved(m, c, r);
            discordClient.ReactionsCleared += async (m, c) => await ReactionCleared(m, c);

            // Bot specific events
            discordClient.CurrentUserUpdated += async (o, n) => await BotUpdated(o, n);
            discordClient.ShardReady += async r => await ShardReady(r);
            discordClient.LoggedIn += async () => await LoggedIn();
        }
    }
}
