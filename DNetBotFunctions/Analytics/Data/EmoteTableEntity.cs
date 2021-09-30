using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace DNetBotFunctions.Analytics.Data
{
    public class EmoteTableEntity : TableEntity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Animated { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string CreatorId { get; set; }
        public bool IsManaged { get; set; }
        public bool RequireColons { get; set; }

        public EmoteTableEntity() { }

        public EmoteTableEntity(string _partitionKey, string _rowKey, DiscordEmote emote)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = emote.ID.ToString();
            Name = emote.Name;
            Url = emote.Url;
            Animated = emote.Animated;
            CreatedAt = emote.CreatedAt;
            CreatorId = emote.CreatorId.ToString();
            IsManaged = emote.IsManaged;
            RequireColons = emote.RequireColons;
        }
    }
}
