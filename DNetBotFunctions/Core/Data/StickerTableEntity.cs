using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;

namespace DNetBotFunctions.Core.Data
{
    public class StickerTableEntity : TableEntity
    {
        public string ID { get; set; }
        public string GuildID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool? Available { get; set; }
        public ulong PackID { get; set; }
        public ulong? AuthorID { get; set; }

        public int? Format { get; set; }
        public int? SortOrder { get; set; }
        public int Type { get; set; }

        public StickerTableEntity() { }

        public StickerTableEntity(string _partitionKey, string _rowKey, DiscordSticker sticker)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = sticker.ID.ToString();
            GuildID = sticker.GuildID.ToString();
            Name = sticker.Name;
            Description = sticker.Description;
            Available = sticker.Available;
            PackID = sticker.PackID;
            AuthorID = sticker.AuthorID;
            Format = sticker.Format;
            SortOrder = sticker.SortOrder;
            Type = sticker.Type;
        }
    }
}
