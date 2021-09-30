using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetBotFunctions.Analytics.Data
{
    public class ChannelTableEntity : TableEntity
    {
        public string ID { get; set; }
        public int Type { get; set; }
        public string GuildID { get; set; }
        public int? Position { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public bool? NSFW { get; set; }
        public int? BitRate { get; set; }
        public int? UserLimit { get; set; }
        public int? RateLimit { get; set; }
        public string CategoryID { get; set; }
        public string Mention { get; set; }
        public int ChannelType { get; set; }

        public ChannelTableEntity() { }

        public ChannelTableEntity(string _partitionKey, string _rowKey, ulong _channelID, ulong _guildID)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = _channelID.ToString();
            GuildID = _guildID.ToString();
        }

        public ChannelTableEntity(string _partitionKey, string _rowKey, DiscordChannel channel)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = channel.ID.ToString();
            Type = channel.Type;
            GuildID = channel.GuildID.ToString();
            Position = channel.Position;
            Name = channel.Name;
            Topic = channel.Topic;
            NSFW = channel.NSFW;
            BitRate = channel.BitRate;
            UserLimit = channel.UserLimit;
            RateLimit = channel.RateLimit;
            CategoryID = channel.CategoryID.ToString();
            Mention = channel.Mention;
            ChannelType = (int)channel.ChannelType;
        }
    }
}
