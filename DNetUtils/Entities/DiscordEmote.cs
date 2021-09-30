using Discord;
using Newtonsoft.Json;
using System;

namespace DNetUtils.Entities
{
    public class DiscordEmote
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Animated { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public ulong? CreatorId { get; set; }
        public bool IsManaged { get; set; }
        public bool RequireColons { get; set; }

        public DiscordEmote(Emote emote)
        {
            ID = emote.Id;
            Name = emote.Name;
            Url = emote.Url;
            Animated = emote.Animated;
            CreatedAt = emote.CreatedAt;
        }

        public DiscordEmote(GuildEmote emote)
        {
            ID = emote.Id;
            Name = emote.Name;
            Url = emote.Url;
            Animated = emote.Animated;
            CreatedAt = emote.CreatedAt;
            CreatorId = emote.CreatorId;
            IsManaged = emote.IsManaged;
            RequireColons = emote.RequireColons;
        }

        public DiscordEmote(string json)
        {
            var emote = JsonConvert.DeserializeObject<DiscordEmote>(json);

            ID = emote.ID;
            Name = emote.Name;
            Url = emote.Url;
            Animated = emote.Animated;
            CreatedAt = emote.CreatedAt;
            CreatorId = emote.CreatorId;
            IsManaged = emote.IsManaged;
            RequireColons = emote.RequireColons;
        }

        /// <summary> 
        /// Returns the Attachment as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
