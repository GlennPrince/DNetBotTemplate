using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
using DNetUtils.Helpers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.Storage.Queue;
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
        private Task RecieveMessage(SocketMessage message)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", message.Source.ToString() + " : " + message.Content);
            
            CloudQueueMessage jsonMessage = new CloudQueueMessage(DiscordConvert.SerializeObject(message));

            try
            {
                inboundQueue.AddMessage(jsonMessage);
            }
            catch (Exception ex)
            {
                Formatter.GenerateLog(_logger, LogSeverity.Error, "QueueMessage", "Unable to add message to Queue: " + inboundQueue.Name 
                    + " | Error: " + ex.Message + " | Inner: " + ex.InnerException.Message);
            }

            return Task.CompletedTask;
        }

        private async Task ProcessMessage(Message message, CancellationToken token)
        {
            var bodyString = Encoding.UTF8.GetString(message.Body);
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Self", "Sending message - Sequence: " + message.SystemProperties.SequenceNumber + " -- Message: " + bodyString);

            try
            {
                NewMessage response = JsonConvert.DeserializeObject(bodyString, typeof(NewMessage)) as NewMessage;
                var channel = discordClient.GetChannel(response.ChannelId);

                ITextChannel textChannel = channel as ITextChannel;
                if (textChannel != null)
                {
                    await textChannel.SendMessageAsync(response.Content);
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

            await servicebusClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
