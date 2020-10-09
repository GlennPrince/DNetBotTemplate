using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
using DNetUtils.Helpers;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        // Handles any actions when a message is received by the bot
        private Task ReceiveMessage(SocketMessage message)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "New Message From : " + message.Source.ToString() + " | Message Content: " + message.Content);
            var serializedMessage = DiscordConvert.SerializeObject(message);

            return SendEvent("messages", "NewMessage", "GameMasterBot.Message", serializedMessage);
        }

        private async Task SendMessage(NewMessage message)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Sending message -- Channel: " + message.ChannelId + " -- Content: " + message.Content);

            try
            {
                var channel = discordClient.GetChannel(message.ChannelId);

                ITextChannel textChannel = channel as ITextChannel;
                if (textChannel != null)
                {
                    await textChannel.SendMessageAsync(message.Content);
                }
                else
                {
                    Formatter.GenerateLog(_logger, LogSeverity.Error, "Self", "Error sending message: Channel is not a text channel");
                }
            }
            catch (Exception ex)
            {
                Formatter.GenerateLog(_logger, LogSeverity.Error, "Self", "Error sending message: " + ex.Message);
            }
        }
    }
}
