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
        #region Guild Configuration

        /// <summary> 
        /// The Id / Snowflake of this guild. 
        /// </summary>
        public ulong Id { get; set; }
        /// <summary> 
        /// The name of this guild. 
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        /// The description of this guild. 
        /// </summary>
        public string Description { get; set; }
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

        public string DiscoverySplashId { get; set; }
        public string DiscoverySplashUrl { get; set; }
        /// <summary> 
        /// The Id for the Guild banner image 
        /// </summary>
        public string BannerId { get; set; }
        /// <summary> 
        /// The URL for the Guild banner image 
        /// </summary>
        public string BannerURL { get; set; }
        /// <summary> 
        /// The amount of time (in seconds) a user must be inactive in a voice channel before they are automatically moved to the AFK voice channel. 
        /// </summary>
        public int AFKTimeout { get; set; }
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
        /// The preferred locale of this guild in IETF BCP 47 language tag format. 
        /// </summary>
        public string PreferredLocale { get; set; }
        /// <summary> 
        /// The preferred culture of this guild. 
        /// </summary>
        public CultureInfo PreferredCulture { get; set; }

        public bool IsBoostProgressBarEnabled { get; }
        /// <summary> 
        /// The NSFW Level for the Guild(0 = Default, 1 = Explicit, 2 = Safe, 3 = Age Restricted). 
        /// </summary>
        public int NsfwLevel { get; set; }
        public bool IsWidgetEnabled { get; set; }

        #endregion

        #region Guild Statistics

        /// <summary> 
        /// The number of Members in the Guild 
        /// </summary>
        public int? MemberCount { get; set; }
        /// <summary> 
        /// Date the Guild was Created 
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary> 
        /// This is the number of users who have boosted this guild. 
        /// </summary>
        public int PremiumSubscriptionCount { get; set; }

        public int? MaxPresences { get; set; }
        public int? MaxMembers { get; set; }
        public int? MaxVideoChannelUsers { get; set; }
        //
        // Summary:
        //     Indicates whether the client is connected to this guild.
        public bool IsConnected { get; set; }
        public bool HasAllMembers { get; set; }
        public ulong MaxUploadLimit { get; set; }
        public int MaxBitrate { get; set; }

        #endregion

        #region Guild Identifiers

        /// <summary> 
        /// Application Id of the guild creator if it is bot-created. 
        /// </summary>
        public ulong? ApplicationId { get; set; }
        /// <summary> 
        /// Owner Id of the guild creator if it is not bot-created. 
        /// </summary>
        public ulong? OwnerId { get; set; }
        /// <summary> 
        /// Id for the everyone role 
        /// </summary>
        public ulong? EveryoneId { get; set; }
        /// <summary> 
        /// Id for the Default SocketTextChannel 
        /// </summary>
        public ulong? DefaultChannelId { get; set; }
        /// <summary> 
        /// Id for the SocketTextChannel that receives randomized welcome messages 
        /// </summary>
        public ulong? SystemChannelId { get; set; }
        /// <summary> 
        /// Id for the channel set within the server's widget settings; null if none is set. 
        /// </summary>
        public ulong? WidgetChannelId { get; set; }
        /// <summary> 
        /// Id for the text channel with the guild rules; null if none is set. 
        /// </summary>
        public ulong? RulesChannelId { get; set; }
        /// <summary> 
        /// Id for the text channel where admins and moderators of Community guilds receive notices from Discord; null if none is set.
        /// </summary>
        public ulong? PublicUpdatesChannelId { get; set; }
        /// <summary> 
        /// Id for the Socket Voice Channel that the AFK users will be moved to after they have idled for too long; null if none is set.
        /// </summary>
        public ulong? AFKChannelId { get; set; }
        /// <summary> 
        /// Voice Region for the Server 
        /// </summary>
        public string VoiceRegionId { get; set; }
        /// <summary> 
        /// List of the Guilds TextChannels Id's 
        /// </summary>
        public ICollection<ulong> TextChannelIds { get; set; }
        /// <summary> 
        /// List of the Guilds SocketVoiceChannel Id's 
        /// </summary>
        public ICollection<ulong> VoiceIds { get; set; }
        /// <summary> 
        /// List of the Guilds Channel Category Id's 
        /// </summary>
        public ICollection<ulong> CategoryIds { get; set; }
        /// <summary> 
        /// List of the Guilds StageChannel Id's 
        /// </summary>
        public ICollection<ulong> StageChannelIds { get; set; }
        /// <summary> 
        /// List of the Guilds Thread Channels Id's 
        /// </summary>
        public ICollection<ulong> ThreadChannelIds { get; set; }
        /// <summary> 
        /// List of the Guilds User Id's 
        /// </summary>
        public ICollection<ulong> UserIds { get; set; }
        /// <summary> 
        /// List of the Guilds Role Id's 
        /// </summary>
        public ICollection<ulong> RoleIds { get; set; }
        /// <summary> 
        /// List of the Guilds Emote Id's 
        /// </summary>
        public ICollection<ulong> EmoteIds { get; set; }
        /// <summary> 
        /// List of the Guilds Sticker Id's 
        /// </summary>
        public ICollection<ulong> StickerIds { get; set; }

        #endregion


        public DiscordGuild() { }

        public DiscordGuild(SocketGuild guild)
        {
            // Guild Configuration Items
            Id = guild.Id;
            Name = guild.Name;
            Description = guild.Description;
            IconId = guild.IconId;
            IconURL = guild.IconUrl;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashUrl;
            DiscoverySplashId = guild.DiscoverySplashId;
            DiscoverySplashUrl = guild.DiscoverySplashUrl;
            BannerId = guild.BannerId;
            BannerURL = guild.BannerUrl;
            AFKTimeout = guild.AFKTimeout;
            DefaultMessageNotifications = (int)guild.DefaultMessageNotifications;
            ExplicitContentFilter = (int)guild.ExplicitContentFilter;
            MfaLevel = (int)guild.MfaLevel;
            VerificationLevel = (int)guild.VerificationLevel;
            PreferredLocale = guild.PreferredLocale.ToString();
            PreferredCulture = guild.PreferredCulture;
            IsBoostProgressBarEnabled = guild.IsBoostProgressBarEnabled;
            NsfwLevel = (int)guild.NsfwLevel;
            IsWidgetEnabled = guild.IsWidgetEnabled;

            // Guild Statistical Items
            MemberCount = guild.MemberCount;
            CreatedAt = guild.CreatedAt;
            PremiumSubscriptionCount = guild.PremiumSubscriptionCount;
            MaxPresences = guild.MaxPresences;
            MaxMembers = guild.MaxMembers;
            MaxVideoChannelUsers = guild.MaxVideoChannelUsers;
            IsConnected = guild.IsConnected;
            HasAllMembers = guild.HasAllMembers;
            MaxUploadLimit = guild.MaxUploadLimit;
            MaxBitrate = guild.MaxBitrate;

            // Guild Identifiers
            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;
            EveryoneId = guild.EveryoneRole.Id;
            if (guild.DefaultChannel != null) 
                DefaultChannelId = guild.DefaultChannel.Id;
            if (guild.SystemChannel != null) 
                SystemChannelId = guild.SystemChannel.Id;
            if(guild.WidgetChannel != null)
                WidgetChannelId = guild.WidgetChannel.Id;
            if (guild.RulesChannel != null) 
                RulesChannelId = guild.RulesChannel.Id;
            if (guild.PublicUpdatesChannel != null) 
                PublicUpdatesChannelId = guild.PublicUpdatesChannel.Id;
            if (guild.AFKChannel != null) 
                AFKChannelId = guild.AFKChannel.Id;
            VoiceRegionId = guild.VoiceRegionId;

            TextChannelIds = new List<ulong>();
            foreach (var channel in guild.TextChannels)
                TextChannelIds.Add(channel.Id);

            VoiceIds = new List<ulong>();
            foreach (var channel in guild.VoiceChannels)
                VoiceIds.Add(channel.Id);

            CategoryIds = new List<ulong>();
            foreach (var channel in guild.CategoryChannels)
                CategoryIds.Add(channel.Id);

            StageChannelIds = new List<ulong>();
            foreach (var channel in guild.StageChannels)
                StageChannelIds.Add(channel.Id);

            ThreadChannelIds = new List<ulong>();
            foreach (var channel in guild.ThreadChannels)
                ThreadChannelIds.Add(channel.Id);

            UserIds = new List<ulong>();
            foreach (var user in guild.Users)
                UserIds.Add(user.Id);

            RoleIds = new List<ulong>();
            foreach (var role in guild.Roles)
                RoleIds.Add(role.Id);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.Emotes)
                EmoteIds.Add(emote.Id);

            StickerIds = new List<ulong>();
            foreach (var sticker in guild.Stickers)
                StickerIds.Add(sticker.Id);
        }

        public DiscordGuild(RestGuild guild)
        {
            // Guild Configuration Items
            Id = guild.Id;
            Name = guild.Name;
            Description = guild.Description;
            IconId = guild.IconId;
            IconURL = guild.IconUrl;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashUrl;
            DiscoverySplashId = guild.DiscoverySplashId;
            DiscoverySplashUrl = guild.DiscoverySplashUrl;
            BannerId = guild.BannerId;
            BannerURL = guild.BannerUrl;
            AFKTimeout = guild.AFKTimeout;
            DefaultMessageNotifications = (int)guild.DefaultMessageNotifications;
            ExplicitContentFilter = (int)guild.ExplicitContentFilter;
            MfaLevel = (int)guild.MfaLevel;
            VerificationLevel = (int)guild.VerificationLevel;
            PreferredLocale = guild.PreferredLocale.ToString();
            PreferredCulture = guild.PreferredCulture;
            IsBoostProgressBarEnabled = guild.IsBoostProgressBarEnabled;
            NsfwLevel = (int)guild.NsfwLevel;
            IsWidgetEnabled = guild.IsWidgetEnabled;

            // Guild Statistical Items
            MemberCount = guild.ApproximateMemberCount;
            CreatedAt = guild.CreatedAt;
            PremiumSubscriptionCount = guild.PremiumSubscriptionCount;
            MaxPresences = guild.MaxPresences;
            MaxMembers = guild.MaxMembers;
            MaxVideoChannelUsers = guild.MaxVideoChannelUsers;
            MaxUploadLimit = guild.MaxUploadLimit;
            MaxBitrate = guild.MaxBitrate;

            // Guild Identifiers
            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;
            EveryoneId = guild.EveryoneRole.Id;
            SystemChannelId = guild.SystemChannelId;
            WidgetChannelId = guild.WidgetChannelId;
            RulesChannelId = guild.RulesChannelId;
            PublicUpdatesChannelId = guild.PublicUpdatesChannelId;
            AFKChannelId = guild.AFKChannelId;
            VoiceRegionId = guild.VoiceRegionId;

            RoleIds = new List<ulong>();
            foreach (var role in guild.Roles)
                RoleIds.Add(role.Id);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.Emotes)
                EmoteIds.Add(emote.Id);

            StickerIds = new List<ulong>();
            foreach (var sticker in guild.Stickers)
                StickerIds.Add(sticker.Id);
        }

        public DiscordGuild(string json)
        {
            var guild = JsonConvert.DeserializeObject<DiscordGuild>(json);

            // Guild Configuration Items
            Id = guild.Id;
            Name = guild.Name;
            Description = guild.Description;
            IconId = guild.IconId;
            IconURL = guild.IconURL;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashURL;
            DiscoverySplashId = guild.DiscoverySplashId;
            DiscoverySplashUrl = guild.DiscoverySplashUrl;
            BannerId = guild.BannerId;
            BannerURL = guild.BannerURL;
            AFKTimeout = guild.AFKTimeout;
            DefaultMessageNotifications = guild.DefaultMessageNotifications;
            ExplicitContentFilter = guild.ExplicitContentFilter;
            MfaLevel = guild.MfaLevel;
            VerificationLevel = guild.VerificationLevel;
            PreferredLocale = guild.PreferredLocale.ToString();
            PreferredCulture = guild.PreferredCulture;
            IsBoostProgressBarEnabled = guild.IsBoostProgressBarEnabled;
            NsfwLevel = guild.NsfwLevel;
            IsWidgetEnabled = guild.IsWidgetEnabled;

            // Guild Statistical Items
            MemberCount = guild.MemberCount;
            CreatedAt = guild.CreatedAt;
            PremiumSubscriptionCount = guild.PremiumSubscriptionCount;
            MaxPresences = guild.MaxPresences;
            MaxMembers = guild.MaxMembers;
            MaxVideoChannelUsers = guild.MaxVideoChannelUsers;
            IsConnected = guild.IsConnected;
            HasAllMembers = guild.HasAllMembers;
            MaxUploadLimit = guild.MaxUploadLimit;
            MaxBitrate = guild.MaxBitrate;

            // Guild Identifiers
            ApplicationId = guild.ApplicationId;
            OwnerId = guild.OwnerId;
            EveryoneId = guild.EveryoneId;
            DefaultChannelId = guild.DefaultChannelId;
            SystemChannelId = guild.SystemChannelId;
            WidgetChannelId = guild.WidgetChannelId;
            RulesChannelId = guild.RulesChannelId;
            PublicUpdatesChannelId = guild.PublicUpdatesChannelId;
            AFKChannelId = guild.AFKChannelId;
            VoiceRegionId = guild.VoiceRegionId;

            TextChannelIds = new List<ulong>();
            foreach (var channel in guild.TextChannelIds)
                TextChannelIds.Add(channel);

            VoiceIds = new List<ulong>();
            foreach (var channel in guild.VoiceIds)
                VoiceIds.Add(channel);

            CategoryIds = new List<ulong>();
            foreach (var channel in guild.CategoryIds)
                CategoryIds.Add(channel);

            StageChannelIds = new List<ulong>();
            foreach (var channel in guild.StageChannelIds)
                StageChannelIds.Add(channel);

            ThreadChannelIds = new List<ulong>();
            foreach (var channel in guild.ThreadChannelIds)
                ThreadChannelIds.Add(channel);

            UserIds = new List<ulong>();
            foreach (var user in guild.UserIds)
                UserIds.Add(user);

            RoleIds = new List<ulong>();
            foreach (var role in guild.RoleIds)
                RoleIds.Add(role);

            EmoteIds = new List<ulong>();
            foreach (var emote in guild.EmoteIds)
                EmoteIds.Add(emote);

            StickerIds = new List<ulong>();
            foreach (var sticker in guild.StickerIds)
                StickerIds.Add(sticker);
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
