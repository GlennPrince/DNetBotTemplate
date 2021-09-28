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
            return null;
        }

        private Task BotUpdated(SocketSelfUser oldBot, SocketSelfUser newBot)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Bot Updated - " + newBot.ToString());
            var botDetails = new DiscordUser(newBot).ToString();
            cachedData.StringSet("bot:" + newBot.Id.ToString(), botDetails);
            return SendEvent("bot", "Update", "DNetBot.Bot.Update", "bot:" + newBot.Id.ToString());
        }

        private Task LoggedIn()
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Client Logged In");
            return SendEvent("bot", "LoggedIn", "DNetBot.Bot.LoggedIn", "");
        }
    }
}
