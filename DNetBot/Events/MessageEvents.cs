using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
using Microsoft.Extensions.Hosting;
using System;
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
            cachedData.StringSet("message:" + message.Channel.Id.ToString() + ":" + message.Id.ToString(), serializedMessage, TimeSpan.FromMinutes(10));

            foreach (var user in message.MentionedUsers)
            {
                var serializedUser = new DiscordUser(user).ToString();
                cachedData.StringSet("message_mentioneduser:" + message.Id.ToString() + ":" + user.Id.ToString(), serializedUser, TimeSpan.FromMinutes(10));
            }

            foreach (var role in message.MentionedRoles)
            {
                var serializedRole = new DiscordRole(role).ToString();
                cachedData.StringSet("message_mentionedrole:" + message.Id.ToString() + ":" + role.Id.ToString(), serializedRole, TimeSpan.FromMinutes(10));
            }

            foreach (var channel in message.MentionedChannels)
            {
                var serializedChannel = new DiscordChannel(channel).ToString();
                cachedData.StringSet("message_mentionedchannel:" + message.Id.ToString() + ":" + channel.Id.ToString(), serializedChannel, TimeSpan.FromMinutes(10));
            }

            foreach (var embed in message.Embeds)
            {
                var serializedRole = new DiscordEmbed(embed).ToString();
                cachedData.StringSet("message_embed:" + message.Id.ToString() + ":" + embed.Title.ToString(), serializedRole, TimeSpan.FromMinutes(10));
            }

            foreach (var attachment in message.Attachments)
            {
                var serializedAttachment = new DiscordAttachment(attachment).ToString();
                cachedData.StringSet("message_attachment:" + message.Id.ToString() + ":" + attachment.Id.ToString(), serializedAttachment, TimeSpan.FromMinutes(10));
            }

            return SendEvent("messages", "NewMessage", "DNetBot.Message.NewMessage", "message:" + message.Channel.Id.ToString() + ":" + message.Id.ToString());
        }

        private Task DeletedMessage(ulong messageId, IMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Message Deleted From Channel ID : " + channel.Id.ToString() + " | Message Id: " + messageId);
            cachedData.KeyDelete("message:" + channel.Id.ToString() + ":" + messageId.ToString());
            return SendEvent("messages", "DeletedMessage", "DNetBot.Message.Deleted", "message:" + channel.Id.ToString() + ":" + messageId.ToString());
        }

        private Task UpdatedMessage(ulong messageId, SocketMessage message, ISocketMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Message Updated From Channel ID : " + channel.Id.ToString() + " | Message Content: " + message.Content);
            var serializedMessage = new DiscordMessage(message).ToString();
            cachedData.StringSet("message:" + message.Channel.Id.ToString() + ":" + message.Id.ToString(), serializedMessage, TimeSpan.FromMinutes(10));

            foreach (var user in message.MentionedUsers)
            {
                var serializedUser = new DiscordUser(user).ToString();
                cachedData.StringSet("message_mentioneduser:" + message.Id.ToString() + ":" + user.Id.ToString(), serializedUser, TimeSpan.FromMinutes(10));
            }

            foreach (var role in message.MentionedRoles)
            {
                var serializedRole = new DiscordRole(role).ToString();
                cachedData.StringSet("message_mentionedrole:" + message.Id.ToString() + ":" + role.Id.ToString(), serializedRole, TimeSpan.FromMinutes(10));
            }

            foreach (var mentionedChannel in message.MentionedChannels)
            {
                var serializedChannel = new DiscordChannel(mentionedChannel).ToString();
                cachedData.StringSet("message_mentionedchannel:" + message.Id.ToString() + ":" + mentionedChannel.Id.ToString(), serializedChannel, TimeSpan.FromMinutes(10));
            }

            foreach (var embed in message.Embeds)
            {
                var serializedRole = new DiscordEmbed(embed).ToString();
                cachedData.StringSet("message_embed:" + message.Id.ToString() + ":" + embed.Title.ToString(), serializedRole, TimeSpan.FromMinutes(10));
            }

            foreach (var attachment in message.Attachments)
            {
                var serializedAttachment = new DiscordAttachment(attachment).ToString();
                cachedData.StringSet("message_attachment:" + message.Id.ToString() + ":" + attachment.Id.ToString(), serializedAttachment, TimeSpan.FromMinutes(10));
            }

            return SendEvent("messages", "UpdatedMessage", "DNetBot.Message.Updated", "message:" + message.Channel.Id.ToString() + ":" + message.Id.ToString());
        }

        private Task ReactionAdded(Cacheable<IUserMessage, ulong> message, IMessageChannel channel, SocketReaction reaction)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reaction Added to Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            var serializedReaction = reaction.ToString();
            cachedData.StringSet("message_reaction:" + message.Id.ToString() + ":" + reaction.UserId.ToString() + ":" + reaction.Emote.Name, serializedReaction, TimeSpan.FromMinutes(10));
            return SendEvent("reaction", "AddReaction", "DNetBot.Reaction.Add", "message_reaction:" + message.Id.ToString() + ":" + reaction.UserId.ToString() + ":" + reaction.Emote.Name);
        }

        private Task ReactionRemoved(Cacheable<IUserMessage, ulong> message, IMessageChannel channel, SocketReaction reaction)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reaction Removed off Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            cachedData.KeyDelete("message_reaction:" + message.Id.ToString() + ":" + reaction.UserId.ToString() + ":" + reaction.Emote.Name);
            return SendEvent("reaction", "RemoveReaction", "DNetBot.Reaction.Delete", "message_reaction:" + message.Id.ToString() + ":" + reaction.UserId.ToString() + ":" + reaction.Emote.Name);
        }

        private Task ReactionCleared(Cacheable<IUserMessage, ulong> message, IMessageChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Message", "Reactions Cleared off Message ID: " + message.Id + " | From Channel ID: " + channel.Id);
            var endpoints = _connectionMultiplexer.GetEndPoints();
            foreach(var endpoint in endpoints)
            {
                var keys = _connectionMultiplexer.GetServer(endpoint).Keys(pattern: "message_reaction: " + message.Id.ToString());
                foreach(var key in keys)
                    cachedData.KeyDelete(key);
            }
            return SendEvent("reaction", "ClearReactions", "DNetBot.Reaction.Clear", "message_reaction:" + message.Id.ToString());
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
