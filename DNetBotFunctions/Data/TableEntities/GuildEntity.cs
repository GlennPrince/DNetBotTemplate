using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace DNetBotFunctions.Clients.TableEntities
{
    public class GuildEntity : TableEntity
    {
        public GuildEntity(DiscordGuild guild)
        {
            PartitionKey = guild.Id.ToString();
            RowKey = "Guild.Info";

            Id = guild.Id.ToString();
            Name = guild.Name;
            IconId = guild.IconId;
            IconURL = guild.IconURL;
            SplashId = guild.SplashId;
            SplashURL = guild.SplashURL;
            MemberCount = guild.MemberCount;

            AFKTimeout = guild.AFKTimeout;
            IsEmbeddable = guild.IsEmbeddable;
            DefaultMessageNotifications = guild.DefaultMessageNotifications;
            ExplicitContentFilter = guild.ExplicitContentFilter;
            MfaLevel = guild.MfaLevel;
            VerificationLevel = guild.VerificationLevel;

            if (guild.ApplicationId.HasValue)
                ApplicationId = guild.ApplicationId.ToString();
            OwnerId = guild.OwnerId.ToString();
            DefaultChannelId = guild.DefaultChannelId.ToString();
            EmbedChannelId = guild.EmbedChannelId.ToString();
            EveryoneId = guild.EveryoneId.ToString();
            SystemChannelId = guild.SystemChannelId.ToString();

            CreatedAt = guild.CreatedAt.UtcDateTime;
            VoiceRegionId = guild.VoiceRegionId;
        }

        /// <summary> 
        /// The Id / Snowflake of this guild. 
        /// </summary>
        public string Id { get; set; }
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
        public string ApplicationId { get; set; }
        /// <summary> 
        /// Owner Id of the guild creator if it is not bot-created. 
        /// </summary>
        public string OwnerId { get; set; }
        /// <summary> 
        /// Id for the Default SocketTextChannel 
        /// </summary>
        public string DefaultChannelId { get; set; }
        /// <summary> 
        /// Id for the Embed Channel (the channel set in the guild's wIdget settings) 
        /// </summary>
        public string EmbedChannelId { get; set; }
        /// <summary> 
        /// Id for the everyone role 
        /// </summary>
        public string EveryoneId { get; set; }
        /// <summary> 
        /// Id for the SocketTextChannel that receives randomized welcome messages 
        /// </summary>
        public string SystemChannelId { get; set; }

        /// <summary> 
        /// Date the Guild was Created 
        /// </summary>
        public DateTime CreatedAt { get; set; }
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
        public string PreferredCulture { get; set; }
    }
}