using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
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
            var serializedMessage = new DiscordMessage(message).ToString();
            return SendEvent("messages", "NewMessage", "DNetBot.Message.NewMessage", serializedMessage);
        }

        private Task DeletedMessage(ulong messageId, ISocketMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Message Deleted From Channel ID : " + channel.Id.ToString() + " | Message Id: " + messageId);
            var newMessage = new DiscordMessage();
            newMessage.ChannelId = channel.Id;
            newMessage.MessageId = messageId;
            return SendEvent("messages", "DeletedMessage", "DNetBot.Message.Deleted", newMessage.ToString());
        }

        private Task UpdatedMessage(ulong messageId, SocketMessage message, ISocketMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Message Updated From Channel ID : " + channel.Id.ToString() + " | Message Content: " + message.Content);
            var serializedMessage = new DiscordMessage(message).ToString();
            return SendEvent("messages", "UpdatedMessage", "DNetBot.Message.Updated", serializedMessage.ToString());
        }

        private Task ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reaction Added to Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            var serializedReaction = reaction.ToString();
            return SendEvent("reaction", "AddReaction", "DNetBot.Reaction.Add", serializedReaction);
        }

        private Task ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reaction Removed off Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            var serializedReaction = reaction.ToString();
            return SendEvent("reaction", "RemoveReaction", "DNetBot.Reaction.Delete", serializedReaction);
        }

        private Task ReactionCleared(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reactions Cleared off Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            return SendEvent("reaction", "ClearReactions", "DNetBot.Reaction.Clear", message.Id.ToString());
        }

        public async Task SendMessage(DiscordMessage message)
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
