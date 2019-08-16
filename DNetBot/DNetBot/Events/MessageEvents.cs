using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        // Handles any actions when a message is received by the bot
        private Task RecieveMessage(SocketMessage message)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", message.Source.ToString() + " : " + message.Content);

            if (message.Content.ToLower().StartsWith("!ping"))
                message.Channel.SendMessageAsync("pong!");

            return Task.CompletedTask;
        }
    }
}
