using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DNetUtils.Entities
{
    /// <summary> 
    /// A DiscordGuild entity as it directly relates to the DiscordGuild entity in the Discord.Net library
    /// This entity is used to serialize/deserialize the DiscordGuild entity to maintain state and other options within the Bot.
    /// </summary>
    public class DiscordGuild
    {
        /// <summary> 
        /// The Id / Snowflake of this guild. 
        /// </summary>
        public ulong Id { get; set; }
        /// <summary> 
        /// The name of this guild. 
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        /// The Id for the Guild Icon 
        /// </summary>
        public string IconId { get; set; }
        /// <summary> 
        /// The URL for the Guild Icon 
        /// </summary>
        public string IconURL { get; set; }
        /// <summary> 
        /// The Id for the Guild splash image 
        /// </summary>
        public string SplashId { get; set; }
        /// <summary> 
        /// The URL for the Guild splash image 
        /// </summary>
        public string SplashURL { get; set; }
        /// <summary> 
        /// The Id for the Guild banner image 
        /// </summary>
        public string BannerId { get; set; }
        /// <summary> 
        /// The URL for the Guild banner image 
        /// </summary>
        public string BannerURL { get; set; }
        /// <summary> 
        /// The number of Members in the Guild 
        /// </summary>
        public int MemberCount { get; set; }

        /// <summary> 
        /// The amount of time (in seconds) a user must be inactive in a voice channel before they are automatically moved to the AFK voice channel. 
        /// </summary>
        public int AFKTimeout { get; set; }
        /// <summary> 
        /// Indicates whether this guild is embeddable (i.e. can use wIdget). 
        /// </summary>
        public bool IsEmbeddable { get; set; }
        /// <summary> 
        /// The default message notification setting for this Guild (0 = All, 1 = Mentions Only) 
        /// </summary>
        public int DefaultMessageNotifications { get; set; }
        /// <summary> 
        /// Level of Content Filtering setting for this Guild (0 = Disabled, 1 = Members without Roles, 2 = All Members) 
        /// </summary>
        public int ExplicitContentFilter { get; set; }
        /// <summary> 
        /// MFA Requirements for the Guild (0 = Not Required, 1 = Required) 
        /// </summary>
        public int MfaLevel { get; set; }
        /// <summary> 
        /// Verification Requirements for the Guild (0 = None, 1 = Email, 2 = Email + 5 Min Wait, 3 = Email + 10 min wait, 4 = Phone) 
        /// </summary>
        public int VerificationLevel { get; set; }

        /// <summary> 
        /// Application Id of the guild creator if it is bot-created. 
        /// </summary>
        public ulong? ApplicationId { get; set; }
        /// <summary> 
        /// Owner Id of the guild creator if it is not bot-created. 
        /// </summary>
        public ulong OwnerId { get; set; }
        /// <summary> 
        /// Id for the Default SocketTextChannel 
        /// </summary>
        public ulong DefaultChannelId { get; set; }
        /// <summary> 
        /// Id for the Embed Channel (the channel set in the guild's wIdget settings) 
        /// </summary>
        public ulong EmbedChannelId { get; set; }
        /// <summary> 
        /// Id for the everyone role 
        /// </summary>
        public ulong EveryoneId { get; set; }
        /// <summary> 
        /// Id for the SocketTextChannel that receives randomized welcome messages 
        /// </summary>
        public ulong SystemChannelId { get; set; }

        /// <summary> 
        /// List of the Guilds TextChannels Id's 
        /// </summary>
        public ICollection<ulong> ChannelIds { get; set; }
        /// <summary> 
        /// List of the Guilds SocketVoiceChannel Id's 
        /// </summary>
        public ICollection<ulong> VoiceIds { get; set; }
        /// <summary> 
        /// List of the Guilds Channel Category Id's 
        /// </summary>
        public ICollection<ulong> CategoryIds { get; set; }
        /// <summary> 
        /// List of the Guilds Role Id's 
        /// </summary>
        public ICollection<ulong> RoleIds { get; set; }
        /// <summary> 
        /// List of the Guilds Emote Id's 
        /// </summary>
        public ICollection<ulong> EmoteIds { get; set; }

        /// <summary> 
        /// Date the Guild was Created 
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary> 
        /// Voice Region for the Server 
        /// </summary>
        public string VoiceRegionId { get; set; }
        /// <summary> 
        /// Premium Tier for the Guild (0 = None, 1 = Tier 1 Boosts, 2 = Tier 2 Boosts, 3 = Tier 3 Boosts) 
        /// </summary>
        public int PremiumTier { get; set; }
        /// <summary> 
        /// A string containing the vanity invite code for the Server 
        /// </summary>
        public string VanityURLCode { get; set; }
        /// <summary> 
        /// This is the number of users who have boosted this guild. 
        /// </summary>
        public int PremiumSubscriptionCount { get; set; }
        /// <summary> 
        /// The preferred locale of this guild in IETF BCP 47 language tag format. 
        /// </summary>
        public string PreferredLocale { get; set; }
        /// <summary> 
        /// The preferred culture of this guild. 
        /// </summary>
        public CultureInfo PreferredCulture { get; set; }

        public DiscordGuild() { }

        public DiscordGuild(SocketGuild guild)
        {
            Id = guild.Id;
            Name = guild.Name;
            IconId = guild.IconId;
            IconURL = guild.IconUrl;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashUrl;
            MemberCount = guild.MemberCount;

            AFKTimeout = guild.AFKTimeout;
            IsEmbeddable = guild.IsEmbeddable;
            DefaultMessageNotifications = (int)guild.DefaultMessageNotifications;
            ExplicitContentFilter = (int)guild.ExplicitContentFilter;
            MfaLevel = (int)guild.MfaLevel;
            VerificationLevel = (int)guild.VerificationLevel;

            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;
            DefaultChannelId = guild.DefaultChannel.Id;
            if(guild.EmbedChannel != null)
                EmbedChannelId = guild.EmbedChannel.Id;
            EveryoneId = guild.EveryoneRole.Id;
            SystemChannelId = guild.SystemChannel.Id;

            ChannelIds = new List<ulong>();
            foreach (var channel in guild.Channels)
                ChannelIds.Add(channel.Id);

            VoiceIds = new List<ulong>();
            foreach (var category in guild.VoiceChannels)
                VoiceIds.Add(category.Id);

            CategoryIds = new List<ulong>();
            foreach (var category in guild.CategoryChannels)
                CategoryIds.Add(category.Id);

            RoleIds = new List<ulong>();
            foreach (var role in guild.Roles)
                RoleIds.Add(role.Id);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.Emotes)
                EmoteIds.Add(emote.Id);

            CreatedAt = guild.CreatedAt;
            VoiceRegionId = guild.VoiceRegionId;
        }

        public DiscordGuild(RestGuild guild)
        {
            Id = guild.Id;
            Name = guild.Name;
            IconId = guild.IconId;
            IconURL = guild.IconUrl;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashUrl;

            AFKTimeout = guild.AFKTimeout;
            IsEmbeddable = guild.IsEmbeddable;
            DefaultMessageNotifications = (int)guild.DefaultMessageNotifications;
            ExplicitContentFilter = (int)guild.ExplicitContentFilter;
            MfaLevel = (int)guild.MfaLevel;
            VerificationLevel = (int)guild.VerificationLevel;

            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;

            RoleIds = new List<ulong>();
            foreach (var role in guild.Roles)
                RoleIds.Add(role.Id);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.Emotes)
                EmoteIds.Add(emote.Id);

            CreatedAt = guild.CreatedAt;
            VoiceRegionId = guild.VoiceRegionId;
        }

        public DiscordGuild(string json)
        {
            var guild = JsonConvert.DeserializeObject<DiscordGuild>(json);

            Id = guild.Id;
            Name = guild.Name;
            IconId = guild.IconId;
            IconURL = guild.IconURL;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashURL;
            MemberCount = guild.MemberCount;

            AFKTimeout = guild.AFKTimeout;
            IsEmbeddable = guild.IsEmbeddable;
            DefaultMessageNotifications = (int)guild.DefaultMessageNotifications;
            ExplicitContentFilter = (int)guild.ExplicitContentFilter;
            MfaLevel = (int)guild.MfaLevel;
            VerificationLevel = (int)guild.VerificationLevel;

            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;
            DefaultChannelId = guild.DefaultChannelId;
            EmbedChannelId = guild.EmbedChannelId;
            EveryoneId = guild.EveryoneId;
            SystemChannelId = guild.SystemChannelId;

            ChannelIds = new List<ulong>();
            foreach (var channel in guild.ChannelIds)
                ChannelIds.Add(channel);

            VoiceIds = new List<ulong>();
            foreach (var voice in guild.VoiceIds)
                VoiceIds.Add(voice);

            CategoryIds = new List<ulong>();
            foreach (var category in guild.CategoryIds)
                CategoryIds.Add(category);

            RoleIds = new List<ulong>();
            foreach (var role in guild.RoleIds)
                RoleIds.Add(role);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.EmoteIds)
                EmoteIds.Add(emote);

            CreatedAt = guild.CreatedAt;
            VoiceRegionId = guild.VoiceRegionId;
        }

        /// <summary> 
        /// Returns the Guild as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
