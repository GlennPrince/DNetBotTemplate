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
        public string DisplayName { get; set; }
        public string NickName { get; set; }
        public UserStatus Status { get; set; }
        public bool IsBot { get; set; }
        /// <summary> 
        /// Date the User was Created 
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        public string AvatarId { get; set; }
        public string GuildAvatarId { get; set; }
        public string MentionId { get; set; }
        public ulong GuildId { get; set; }
        public int Hierarchy { get; set; }
        public bool IsDeafened { get; set; }
        public bool IsMuted { get; set; }
        public bool? IsPending { get; set; }
        public bool? IsSelfMuted { get; set; }
        public bool? IsSuppressed { get; set; }
        public bool? IsStreaming { get; set; }
        public bool IsSelfDeafened { get; set; }
        public bool? IsVideoing { get; set; }
        public DateTimeOffset? JoinedAt { get; set; }
        public string Nickname { get; set; }
        public ulong Permissions { get; set; }
        public Color? AccentColor { get; set; }
        public UserProperties? PublicFlags { get; set; }
        public ICollection<IActivity> Activities { get; set; }
        public ICollection<ulong> MutualGuildIds { get; set; }
        public ICollection<ulong> RoleIds { get; set; }

        public DiscordUser() { }
        public DiscordUser(SocketUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);

            MutualGuildIds = new List<ulong>();
            foreach (var guild in user.MutualGuilds)
                MutualGuildIds.Add(guild.Id);
        }

        public DiscordUser(RestUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;
            AccentColor = user.AccentColor;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);
        }

        public DiscordUser(SocketGuildUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            DisplayName = user.DisplayName;
            NickName = user.Nickname;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            GuildAvatarId = user.GuildAvatarId;
            GuildId = user.Guild.Id;
            Hierarchy = user.Hierarchy;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            IsSelfDeafened = user.IsSelfDeafened;
            JoinedAt = user.JoinedAt;
            Permissions = user.GuildPermissions.RawValue;
            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;
            IsPending = user.IsPending;
            IsSelfMuted = user.IsSelfMuted;
            IsSuppressed = user.IsSuppressed;
            IsStreaming = user.IsStreaming;
            IsVideoing = user.IsVideoing;
            

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);

            MutualGuildIds = new List<ulong>();
            foreach (var guild in user.MutualGuilds)
                MutualGuildIds.Add(guild.Id);

            RoleIds = new List<ulong>();
            foreach (var role in user.Roles)
                MutualGuildIds.Add(role.Id);
        }

        public DiscordUser(RestGuildUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            DisplayName = user.DisplayName;
            NickName = user.Nickname;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            GuildId = user.GuildId;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            JoinedAt = user.JoinedAt;

            Permissions = user.GuildPermissions.RawValue;

            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;
            AccentColor = user.AccentColor;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);
        }

        public DiscordUser(SocketSelfUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);

            MutualGuildIds = new List<ulong>();
            foreach (var guild in user.MutualGuilds)
                MutualGuildIds.Add(guild.Id);
        }

        public DiscordUser(RestSelfUser user)
        {
            ID = user.Id;
            Discriminator = user.Discriminator;
            Username = user.Username;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            CreatedAt = user.CreatedAt;
            MentionId = user.Mention;
            PublicFlags = user.PublicFlags;
            AccentColor = user.AccentColor;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);
        }

        public DiscordUser(string json)
        {
            var user = JsonConvert.DeserializeObject<DiscordUser>(json);

            ID = user.ID;
            Discriminator = user.Discriminator;
            Username = user.Username;
            DisplayName = user.DisplayName;
            NickName = user.Nickname;
            Status = user.Status;
            IsBot = user.IsBot;
            AvatarId = user.AvatarId;
            GuildId = user.GuildId;
            Hierarchy = user.Hierarchy;
            IsDeafened = user.IsDeafened;
            IsMuted = user.IsMuted;
            IsSelfDeafened = user.IsSelfDeafened;
            IsPending = user.IsPending;
            IsSelfMuted = user.IsSelfMuted;
            IsSuppressed = user.IsSuppressed;
            IsStreaming = user.IsStreaming;
            IsVideoing = user.IsVideoing;
            JoinedAt = user.JoinedAt;
            Permissions = user.Permissions;
            CreatedAt = user.CreatedAt;
            MentionId = user.MentionId;
            PublicFlags = user.PublicFlags;
            AccentColor = user.AccentColor;

            Activities = new List<IActivity>();
            foreach (IActivity activity in user.Activities)
                Activities.Add(activity);

            RoleIds = new List<ulong>();
            foreach (var role in user.RoleIds)
                RoleIds.Add(role);

            MutualGuildIds = new List<ulong>();
            foreach (var guild in user.RoleIds)
                MutualGuildIds.Add(guild);
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
