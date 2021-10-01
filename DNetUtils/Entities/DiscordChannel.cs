using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DNetUtils.Entities
{
    public class DiscordChannel
    {
        // Channel Attributes
        public ulong ID { get; set; }
        public int Type { get; set; }
        public ulong? GuildID { get; set; }
        public int? Position { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public bool? NSFW { get; set; }
        public int? BitRate { get; set; }
        public int? UserLimit { get; set; }
        public int? RateLimit { get; set; }
        public List<ulong> Recipients { get; set; }
        public List<ulong> CategorizedChannels { get; set; }
        public List<ulong> Users { get; set; }
        public ulong? CategoryID { get; set; }
        public string Mention { get; set; }
        public ChannelType ChannelType { get; set; }

        // Basic Channel Types
        public DiscordChannel(SocketChannel channel)
        {
            ID = channel.Id;
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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
            UserLimit = channel.UserLimit;
            CategoryID = channel.CategoryId;
            ChannelType = ChannelType.Voice;
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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
            foreach(var recipient in channel.Recipients)
                Recipients.Add(recipient.Id);
            ChannelType = ChannelType.Group;
            foreach (var user in channel.Users)
                Users.Add(user.Id);
        }

        public DiscordChannel(RestGroupChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            foreach (var recipient in channel.Recipients)
                Recipients.Add(recipient.Id);
            ChannelType = ChannelType.Group;
        }

        public DiscordChannel(SocketCategoryChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            foreach (var childChannel in channel.Channels)
                CategorizedChannels.Add(childChannel.Id);
            Position = channel.Position;
            GuildID = channel.Guild.Id;
            ChannelType = ChannelType.Category;
            foreach (var user in channel.Users)
                Users.Add(user.Id);
        }

        public DiscordChannel(RestCategoryChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            Position = channel.Position;
            ChannelType = ChannelType.Category;
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
            foreach (var user in channel.Users)
                Users.Add(user.Id);
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

            foreach (var childChannel in channel.CategorizedChannels)
                CategorizedChannels.Add(childChannel);

            foreach (var recipient in channel.Recipients)
                Recipients.Add(recipient);

            foreach (var user in channel.Users)
                Users.Add(user);

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
