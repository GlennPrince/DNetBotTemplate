using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace DNetBotFunctions.Core.Data
{
    public class MessageTableEntity : TableEntity
    {
        /// <summary> 
        /// The Id / Snowflake of this message. 
        /// </summary>
        public string MessageId { get; set; }
        /// <summary> 
        /// The Id / Snowflake of the author of this message. 
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary> 
        /// The Id / Snowflake of the channel this message was posted in.
        /// </summary>
        public string ChannelId { get; set; }
        /// <summary> 
        /// The source of the message - System, User, Bot or Webhook.
        /// </summary>
        public int Source { get; set; }
        /// <summary> 
        /// The content of the message. IE. the message as seen in Discord
        /// </summary>
        public string Content { get; set; }
        /// <summary> 
        /// The creation date of the message 
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary> 
        /// A flag that determines if the message has been pinned
        /// </summary>
        public bool IsPinned { get; set; }

        public MessageTableEntity() { }

        public MessageTableEntity(string _partitionKey, string _rowKey, DiscordMessage message)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            MessageId = message.MessageId.ToString();
            AuthorId = message.AuthorId.ToString();
            ChannelId = message.ChannelId.ToString();
            Source = (int)message.Source;
            Content = message.Content;
            CreatedAt = message.CreatedAt.UtcDateTime;
            IsPinned = message.IsPinned;
        }
    }
}
