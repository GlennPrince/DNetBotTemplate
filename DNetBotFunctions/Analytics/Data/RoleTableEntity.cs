using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetBotFunctions.Analytics.Data
{
    public class RoleTableEntity : TableEntity
    {
        public string ID { get; set; }
        public string GuildID { get; set; }
        public string Name { get; set; }

        public string Color { get; set; }
        public string Permissions { get; set; }
        public string Mention { get; set; }
        public int Position { get; set; }

        public bool IsManaged { get; set; }
        public bool IsEveryone { get; set; }
        public bool IsMentionable { get; set; }

        public RoleTableEntity() { }

        public RoleTableEntity(string _partitionKey, string _rowKey, DiscordRole role)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = role.ID.ToString();
            GuildID = role.GuildID.ToString();
            Name = role.Name;
            Color = role.Color.RawValue.ToString();
            Permissions = role.Permissions.ToString();
            Mention = role.Mention;
            Position = role.Position;
            IsManaged = role.IsManaged;
            IsEveryone = role.IsEveryone;
            IsMentionable = role.IsMentionable;
        }
    }
}
