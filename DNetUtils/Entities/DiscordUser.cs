using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetUtils.Entities
{
    public class DiscordUser
    {
        public ulong ID { get; set; }
        public string Discriminator { get; set; }
        public string Username { get; set; }
        public UserStatus Status { get; set; }
        public bool IsBot { get; set; }

        public ActivityType ActivityType { get; set; }
        public string ActivityName { get; set; }

        public string AvatarId { get; set; }
        public ulong GuildId { get; set; }
        public int Hierarchy { get; set; }
        public bool IsDeafened { get; set; }
        public bool IsMuted { get; set; }
        public bool IsSelfDeafened { get; set; }
        public DateTimeOffset? JoinedAt { get; set; }
        public string Nickname { get; set; }
        public ulong Permissions { get; set; }

        public DiscordUser() { }
        public DiscordUser(SocketUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }
        }

        public DiscordUser(RestUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }
        }

        public DiscordUser(SocketGuildUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }

            AvatarId = user.AvatarId;
            GuildId = user.Guild.Id;
            Hierarchy = user.Hierarchy;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            IsSelfDeafened = user.IsSelfDeafened;
            JoinedAt = user.JoinedAt;
            Permissions = user.GuildPermissions.RawValue;
        }

        public DiscordUser(RestGuildUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }

            AvatarId = user.AvatarId;
            GuildId = user.GuildId;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            JoinedAt = user.JoinedAt;

            Permissions = user.GuildPermissions.RawValue;
        }

        public DiscordUser(SocketSelfUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }
        }

        public DiscordUser(RestSelfUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            if (user.Activity != null)
            {
                ActivityType = user.Activity.Type;
                ActivityName = user.Activity.Name;
            }
        }

        public DiscordUser(string json)
        {
            var user = JsonConvert.DeserializeObject<DiscordUser>(json);

            ID = user.ID;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;

            ActivityType = user.ActivityType;
            ActivityName = user.ActivityName;

            AvatarId = user.AvatarId;
            GuildId = user.GuildId;
            Hierarchy = user.Hierarchy;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            IsSelfDeafened = user.IsSelfDeafened;
            JoinedAt = user.JoinedAt;
            Permissions = user.Permissions;
        }

        /// <summary> 
        /// Returns the User as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
