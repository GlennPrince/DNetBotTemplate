using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetUtils.Entities
{
    public class ConvertedMessage
    {
        public ulong MessageId { get; set; }
        public ulong AuthorId { get; set; }
        public ulong ChannelId { get; set; }
        public MessageSource Source { get; set; } // System, User, Bot, Webhook
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ICollection<ulong> MentionedChannelIDs { get; set; }
        public ICollection<ulong> MentionedRoleIDs { get; set; }
        public ICollection<ulong> MentionedUserIDs { get; set; }

        public ConvertedMessage() { }

        public ConvertedMessage(SocketMessage message)
        {
            MessageId = message.Id;
            AuthorId = message.Author.Id;
            ChannelId = message.Channel.Id;
            Source = message.Source;
            Content = message.Content;
            CreatedAt = message.CreatedAt;

            MentionedChannelIDs = new List<ulong>();
            MentionedRoleIDs = new List<ulong>();
            MentionedUserIDs = new List<ulong>();

            foreach (var channel in message.MentionedChannels)
            {
                MentionedChannelIDs.Add(channel.Id);
            }

            foreach (var role in message.MentionedRoles)
            {
                MentionedRoleIDs.Add(role.Id);
            }

            foreach (var user in message.MentionedUsers)
            {
                MentionedUserIDs.Add(user.Id);
            }
        }
    }
}
