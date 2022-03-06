using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace DNetBotFunctions.Core.Data
{
    public class AttachmentTableEntity : TableEntity
    {
        public string ID { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string ProxyUrl { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public AttachmentTableEntity() { }

        public AttachmentTableEntity(string _partitionKey, string _rowKey, DiscordAttachment attachment)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;

            ID = attachment.ID.ToString();
            FileName = attachment.FileName;
            Url = attachment.Url;
            ProxyUrl = attachment.ProxyUrl;
            Size = attachment.Size;
            Height = attachment.Height;
            Width = attachment.Width;
        }
    }
}
