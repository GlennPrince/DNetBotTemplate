using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        private Task ShardReady(DiscordSocketClient client)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Shard Ready - Shard ID:" + client.ShardId);

            var discordClient = new DiscordClient(client);
            return SendEvent("client", "ClientReady", "GMBot.Client.Ready", discordClient.ToString());
        }

        private Task BotUpdated(SocketSelfUser oldBot, SocketSelfUser newBot)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Bot Updated - " + newBot.ToString());

            var botDetails = new DiscordUser(newBot);
            return SendEvent("bot", "Update", "GMBot.Bot.Update", botDetails.ToString());
        }

        private Task LoggedIn()
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Client Logged In");

            return SendEvent("bot", "LoggedIn", "GMBot.Bot.LoggedIn", "");
        }
    }
}
