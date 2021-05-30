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
        public ulong? GuildID { get; set; }
        public int? Position { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public bool? NSFW { get; set; }
        public int? BitRate { get; set; }
        public int? UserLimit { get; set; }
        public int? RateLimit { get; set; }
        public List<ulong> Recipients { get; set; }
        public ulong? CategoryID { get; set; }

        // Basic Channel Types
        public DiscordChannel(SocketChannel channel)
        {
            ID = channel.Id;
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
        }

        // DM Channels
        public DiscordChannel(SocketDMChannel channel)
        {
            ID = channel.Id;
            Recipients.Add(channel.Recipient.Id);
        }

        public DiscordChannel(RestDMChannel channel)
        {
            ID = channel.Id;
            Recipients.Add(channel.Recipient.Id);
        }
        // Group DM Channels
        public DiscordChannel(SocketGroupChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            foreach(var recipient in channel.Recipients)
                Recipients.Add(recipient.Id);
        }

        public DiscordChannel(RestGroupChannel channel)
        {
            ID = channel.Id;
            Name = channel.Name;
            foreach (var recipient in channel.Recipients)
                Recipients.Add(recipient.Id);
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
