using Discord;
using Newtonsoft.Json;

namespace DNetUtils.Entities
{
    public class DiscordAttachment
    {
        public ulong ID { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string ProxyUrl { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public DiscordAttachment(Attachment attachment)
        {
            ID = attachment.Id;
            FileName = attachment.Filename;
            Url = attachment.Url;
            ProxyUrl = attachment.ProxyUrl;
            Size = attachment.Size;

            if (attachment.Height.HasValue)
                Height = attachment.Height.Value;
            else
                Height = 0;

            if (attachment.Width.HasValue)
                Width = attachment.Width.Value;
            else
                Width = 0;
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
