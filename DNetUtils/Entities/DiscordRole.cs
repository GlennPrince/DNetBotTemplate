using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DNetUtils.Entities
{
    public class DiscordRole
    {
        public ulong ID { get; set; }
        public ulong GuildID { get; set; }
        public string Name { get; set; }

        public Color Color { get; set; }
        public ulong Permissions { get; set; }
        public string Mention { get; set; }
        public int Position { get; set; }

        public bool IsManaged { get; set; }
        public bool IsEveryone { get; set; }
        public bool IsMentionable { get; set; }

        public ICollection<ulong> MemberIDs { get; set; }

        public DiscordRole(SocketRole role)
        {
            ID = role.Id;
            GuildID = role.Guild.Id;
            Name = role.Name;
            Color = role.Color;
            Permissions = role.Permissions.RawValue;
            Mention = role.Mention;
            Position = role.Position;

            IsEveryone = role.IsEveryone;
            IsManaged = role.IsManaged;
            IsMentionable = role.IsMentionable;
            
            MemberIDs = new List<ulong>();
            foreach (var member in role.Members)
                MemberIDs.Add(member.Id);
        }

        public DiscordRole(RestRole role)
        {
            ID = role.Id;
            Name = role.Name;
            Color = role.Color;
            Permissions = role.Permissions.RawValue;
            Mention = role.Mention;
            Position = role.Position;

            IsEveryone = role.IsEveryone;
            IsManaged = role.IsManaged;
            IsMentionable = role.IsMentionable;
        }

        /// <summary> 
        /// Returns the Role as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
