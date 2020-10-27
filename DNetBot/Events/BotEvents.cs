using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        private Task ShardReady(DiscordSocketClient client)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Shard Ready - Shard ID:" + client.ShardId);

            //return SendEvent("messages", "NewMessage", "DNetBot.Message.NewMessage", serializedMessage);
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
