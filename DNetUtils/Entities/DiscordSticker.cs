using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DNetUtils.Entities
{
    public class DiscordSticker
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong PackID { get; set; }
        public int Format { get; set; }
        public bool? Available { get; set; }
        public int? SortOrder { get; set; }
        public int Type { get; set; }
        public ulong? GuildID { get; set; }
        public ulong? AuthorID { get; set; }

        public DiscordSticker() { }

        public DiscordSticker(Sticker sticker)
        {
            ID = sticker.Id;
            Name = sticker.Name;
            Description = sticker.Description;
            PackID = sticker.PackId;
            Format = (int)sticker.Format;
            Available = sticker.IsAvailable;
            SortOrder = sticker.SortOrder;
            Type = (int)sticker.Type;
        }

        public DiscordSticker(SocketSticker sticker)
        {
            ID = sticker.Id;
            Name = sticker.Name;
            Description = sticker.Description;
            PackID = sticker.PackId;
            Format = (int)sticker.Format;
            Available = sticker.IsAvailable;
            SortOrder = sticker.SortOrder;
            Type = (int)sticker.Type;
        }

        public DiscordSticker(CustomSticker sticker)
        {
            ID = sticker.Id;
            Name = sticker.Name;
            Description = sticker.Description;
            PackID = sticker.PackId;
            Format = (int)sticker.Format;
            Available = sticker.IsAvailable;
            SortOrder = sticker.SortOrder;
            Type = (int)sticker.Type;
            AuthorID = sticker.AuthorId;
            GuildID = sticker.Guild.Id;
        }

        public DiscordSticker(SocketCustomSticker sticker)
        {
            ID = sticker.Id;
            Name = sticker.Name;
            Description = sticker.Description;
            PackID = sticker.PackId;
            Format = (int)sticker.Format;
            Available = sticker.IsAvailable;
            SortOrder = sticker.SortOrder;
            Type = (int)sticker.Type;
            AuthorID = sticker.AuthorId;
            GuildID = sticker.Guild.Id;
        }

        public DiscordSticker(string json)
        {
            var sticker = JsonConvert.DeserializeObject<DiscordSticker>(json);

            ID = sticker.ID;
            Name = sticker.Name;
            Description = sticker.Description;
            PackID = sticker.PackID;
            Format = sticker.Format;
            Available = sticker.Available;
            SortOrder = sticker.SortOrder;
            Type = sticker.Type;
            AuthorID = sticker.AuthorID;
            GuildID = sticker.GuildID;

        }

        /// <summary> 
        /// Returns the Role as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
