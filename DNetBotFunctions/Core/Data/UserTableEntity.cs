using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace DNetBotFunctions.Core.Data
{
    public class UserTableEntity : TableEntity
    {
        public string ID { get; set; }
        public string Discriminator { get; set; }
        public string Username { get; set; }
        public int Status { get; set; }
        public bool IsBot { get; set; }

        public int ActivityType { get; set; }
        public string ActivityName { get; set; }

        public string AvatarId { get; set; }
        public string GuildId { get; set; }
        public int Hierarchy { get; set; }
        public bool IsDeafened { get; set; }
        public bool IsMuted { get; set; }
        public bool IsSelfDeafened { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Nickname { get; set; }
        public string Permissions { get; set; }

        public UserTableEntity() { }

        public UserTableEntity(string _partitionKey, string _rowKey, DiscordUser user)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = user.ID.ToString();
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = (int)user.Status;
            IsBot = user.IsBot;
            ActivityType = (int)user.ActivityType;
            ActivityName = user.ActivityName;
            AvatarId = user.AvatarId;
            GuildId = user.GuildId.ToString();
            Hierarchy = user.Hierarchy;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            IsSelfDeafened = user.IsSelfDeafened;
            JoinedAt = user.JoinedAt.Value.UtcDateTime;
            Nickname = user.Nickname;
            Permissions = user.Permissions.ToString();
        }
    }
}