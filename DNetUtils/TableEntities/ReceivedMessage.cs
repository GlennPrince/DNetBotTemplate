using Discord;
using DNetUtils.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetUtils.TableEntities
{
    public class ReceivedMessage
    {
        public string PartitionKey { get; set; }        // Channel ID
        public string RowKey { get; set; }              // Message ID
        public string AuthorId { get; set; }
        public int Source { get; set; }                 // System, User, Bot, Webhook
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public ReceivedMessage(ConvertedMessage message)
        {
            PartitionKey = message.ChannelId.ToString();
            RowKey = message.MessageId.ToString();
            AuthorId = message.AuthorId.ToString();
            Source = message.Source.GetHashCode();
            Content = message.Content;
            CreatedAt = message.CreatedAt.UtcDateTime;
        }
    }
}
