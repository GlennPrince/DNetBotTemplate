using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DNetUtils.Entities
{
    public class DiscordChannel
    {
        // Channel Attributes
        public ulong ID { get; set; }
        public int Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ulong? GuildID { get; set; }
        public int? Position { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public bool? NSFW { get; set; }
        public int? BitRate { get; set; }
        public int? UserLimit { get; set; }
        public int? RateLimit { get; set; }
        public bool? IsLive  { get; set; }
        public bool? DiscoverableDisabled { get; set; }
        public int PrivacyLevel { get; set; }
        public List<ulong> Recipients { get; set; }
        public List<ulong> CategorizedChannels { get; set; }
        public List<ulong> Users { get; set; }
        public List<ulong> Speakers { get; set; }
        public ulong? CategoryID { get; set; }
        public string Mention { get; set; }
        public ChannelType ChannelType { get; set; }

        public DiscordChannel() { }

        // Basic Channel Types
        public DiscordChannel(SocketChannel channel)
        {
            ID = channel.Id;
            if(channel.Users != null)
            { 
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
        }

        public DiscordChannel(RestChannel channel)
        {
            ID = channel.Id;
        }

        // Basic Guild Channels
        public DiscordChannel(SocketGuildChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.Guild.Id;
            Name = channel.Name;
            Position = channel.Position;
            CreatedAt = channel.CreatedAt;
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
        }

        public DiscordChannel(RestGuildChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.GuildId;
            Name = channel.Name;
            Position = channel.Position;
        }

        // Guild Text Channels
        public DiscordChannel(SocketTextChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.Guild.Id;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            RateLimit = channel.SlowModeInterval;
            CategoryID = channel.CategoryId;
            NSFW = channel.IsNsfw;
            Mention = channel.Mention;
            ChannelType = ChannelType.Text;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
        }

        public DiscordChannel(RestTextChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.GuildId;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            RateLimit = channel.SlowModeInterval;
            CategoryID = channel.CategoryId;
            NSFW = channel.IsNsfw;
            Mention = channel.Mention;
            ChannelType = ChannelType.Text;
        }

        // Guild Voice Channels
        public DiscordChannel(SocketVoiceChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.Guild.Id;
            Name = channel.Name;
            Position = channel.Position;
            BitRate = channel.Bitrate;
            CreatedAt = channel.CreatedAt;
            Mention = channel.Mention;
            UserLimit = channel.UserLimit;
            CategoryID = channel.CategoryId;
            ChannelType = ChannelType.Voice;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
        }

        public DiscordChannel(RestVoiceChannel channel)
        {
            ID = channel.Id;
            GuildID = channel.GuildId;
            Name = channel.Name;
            Position = channel.Position;
            BitRate = channel.Bitrate;
            UserLimit = channel.UserLimit;
            CategoryID = channel.CategoryId;
            ChannelType = ChannelType.Voice;
        }

        // DM Channels
        public DiscordChannel(SocketDMChannel channel)
        {
            ID = channel.Id;
            Recipients.Add(channel.Recipient.Id);
            ChannelType = ChannelType.DM;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(RestDMChannel channel)
        {
            ID = channel.Id;
            Recipients.Add(channel.Recipient.Id);
            ChannelType = ChannelType.DM;
        }
        // Group DM Channels
        public DiscordChannel(SocketGroupChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Recipients = new List<ulong>();
            if (channel.Recipients != null)
            {
                foreach (var recipient in channel.Recipients)
                    Recipients.Add(recipient.Id);
            }
            ChannelType = ChannelType.Group;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(RestGroupChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Recipients = new List<ulong>();
            if (channel.Recipients != null)
            {
                foreach (var recipient in channel.Recipients)
                    Recipients.Add(recipient.Id);
            }
            ChannelType = ChannelType.Group;
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(SocketCategoryChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Position = channel.Position;
            GuildID = channel.Guild.Id;
            ChannelType = ChannelType.Category;
            CategorizedChannels = new List<ulong>();
            if (channel.Channels != null)
            {
                foreach (var childChannel in channel.Channels)
                    CategorizedChannels.Add(childChannel.Id);
            }
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(RestCategoryChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Position = channel.Position;
            ChannelType = ChannelType.Category;
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(SocketNewsChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            GuildID = channel.Guild.Id;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            RateLimit = channel.SlowModeInterval;
            CategoryID = channel.CategoryId;
            NSFW = channel.IsNsfw;
            Mention = channel.Mention;
            ChannelType = ChannelType.News;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(RestNewsChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            RateLimit = channel.SlowModeInterval;
            CategoryID = channel.CategoryId;
            NSFW = channel.IsNsfw;
            Mention = channel.Mention;
            ChannelType = ChannelType.News;
        }

        public DiscordChannel(SocketStageChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            GuildID = channel.Guild.Id;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            BitRate = channel.Bitrate;
            IsLive = channel.IsLive;
            DiscoverableDisabled = channel.IsDiscoverableDisabled;
            PrivacyLevel = (int)channel.PrivacyLevel;
            UserLimit = channel.UserLimit;
            CategoryID = channel.CategoryId;
            Mention = channel.Mention;
            ChannelType = ChannelType.Stage;
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user.Id);
            }
            if (channel.Speakers != null)
            {
                foreach (var user in channel.Users)
                    Speakers.Add(user.Id);
            }
            CreatedAt = channel.CreatedAt;
        }

        public DiscordChannel(string json)
        {
            var channel = JsonConvert.DeserializeObject<DiscordChannel>(json);

            ID = channel.ID;
            GuildID = channel.GuildID;
            Name = channel.Name;
            Position = channel.Position;
            Topic = channel.Topic;
            RateLimit = channel.RateLimit;
            CategoryID = channel.CategoryID;
            NSFW = channel.NSFW;
            Mention = channel.Mention;

            BitRate = channel.BitRate;
            UserLimit = channel.UserLimit;
            CategorizedChannels = new List<ulong>();
            if (channel.CategorizedChannels != null)
            {
                foreach (var childChannel in channel.CategorizedChannels)
                    CategorizedChannels.Add(childChannel);
            }
            Recipients = new List<ulong>();
            if (channel.Recipients != null)
            {
                foreach (var recipient in channel.Recipients)
                    Recipients.Add(recipient);
            }
            Users = new List<ulong>();
            if (channel.Users != null)
            {
                foreach (var user in channel.Users)
                    Users.Add(user);
            }

            ChannelType = channel.ChannelType;
        }

        /// <summary> 
        /// Returns the Channel as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
