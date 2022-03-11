using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetBotFunctions.Core.Data
{
    public class GuildTableEntity : TableEntity
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
        public DateTime CreatedAt { get; set; }

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

        #endregion


        public GuildTableEntity() { }

        public GuildTableEntity(string _partitionKey, string _rowKey, DiscordGuild guild)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

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
            IsBoostProgressBarEnabled = guild.IsBoostProgressBarEnabled;
            NsfwLevel = guild.NsfwLevel;
            IsWidgetEnabled = guild.IsWidgetEnabled;

            // Guild Statistical Items
            MemberCount = guild.MemberCount;
            CreatedAt = guild.CreatedAt.UtcDateTime;
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
        }
    }
}
