using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetUtils.Entities
{
    public class DiscordEmbed
    {
        public EmbedType Type { get; }
        public string Description { get; }
        public string Url { get; }
        public string Title { get; }
        public DateTimeOffset? Timestamp { get; }
        public uint? Color { get; }
        // Embed Image
        public string ImageUrl { get; }
        public string ImageProxyUrl { get; }
        public int? ImageHeight { get; }
        public int? ImageWidth { get; }
        // Embed Video
        public string VideoUrl { get; }
        public int? VideoHeight { get; }
        public int? VideoWidth { get; }
        // Embed Author
        public string AuthorName { get; }
        public string AuthorUrl { get; }
        public string AuthorIconUrl { get; }
        public string AuthorProxyIconUrl { get; }
        // Embed Footer
        public string FooterText { get; }
        public string FooterIconUrl { get; }
        public string FooterProxyUrl { get; }
        // Embed Provider
        public string ProviderName { get; }
        public string ProviderUrl { get; }
        // Embed Thumbnail
        public string ThumbnailUrl { get; }
        public string ThumbnailProxyUrl { get; }
        public int? ThumbnailHeight { get; }
        public int? ThumbnailWidth { get; }

        public DiscordEmbed() { }
        public DiscordEmbed(Embed embed)
        {
            Type = embed.Type;
            Description = embed.Description;
            Url = embed.Url;
            Title = embed.Title;
            Timestamp = embed.Timestamp;
            if (embed.Color.HasValue)
                Color = embed.Color.Value.RawValue;
            if (embed.Image.HasValue)
            {
                ImageUrl = embed.Image.Value.Url;
                ImageProxyUrl = embed.Image.Value.ProxyUrl;
                ImageHeight = embed.Image.Value.Height;
                ImageWidth = embed.Image.Value.Width;
            }
            if (embed.Video.HasValue)
            {
                VideoUrl = embed.Video.Value.Url;
                VideoHeight = embed.Image.Value.Height;
                VideoWidth = embed.Image.Value.Width;
            }
            if (embed.Author.HasValue)
            {
                AuthorUrl = embed.Author.Value.Url;
                AuthorName = embed.Author.Value.Name;
                AuthorIconUrl = embed.Author.Value.IconUrl;
                AuthorProxyIconUrl = embed.Author.Value.ProxyIconUrl;
            }
            if (embed.Footer.HasValue)
            {
                FooterText = embed.Footer.Value.Text;
                FooterIconUrl = embed.Footer.Value.IconUrl;
                FooterProxyUrl = embed.Footer.Value.ProxyUrl;
            }
            if (embed.Provider.HasValue)
            {
                ProviderName = embed.Provider.Value.Name;
                ProviderUrl = embed.Provider.Value.Url;
            }
            if (embed.Thumbnail.HasValue)
            {
                ThumbnailUrl = embed.Thumbnail.Value.Url;
                ThumbnailProxyUrl = embed.Thumbnail.Value.ProxyUrl;
                ThumbnailHeight = embed.Thumbnail.Value.Height;
                ThumbnailWidth = embed.Thumbnail.Value.Width;
            }
        }

        public DiscordEmbed(string json)
        {
            var embed = JsonConvert.DeserializeObject<DiscordEmbed>(json);

            Type = embed.Type;
            Description = embed.Description;
            Url = embed.Url;
            Title = embed.Title;
            Timestamp = embed.Timestamp;
            Color = embed.Color;
            ImageUrl = embed.ImageUrl;
            ImageProxyUrl = embed.ImageProxyUrl;
            ImageHeight = embed.ImageHeight;
            ImageWidth = embed.ImageWidth;
            VideoUrl = embed.VideoUrl;
            VideoHeight = embed.VideoHeight;
            VideoWidth = embed.VideoWidth;
            AuthorUrl = embed.AuthorUrl;
            AuthorName = embed.AuthorName;
            AuthorIconUrl = embed.AuthorIconUrl;
            AuthorProxyIconUrl = embed.AuthorProxyIconUrl;
            FooterText = embed.FooterText;
            FooterIconUrl = embed.FooterIconUrl;
            FooterProxyUrl = embed.FooterProxyUrl;
            ProviderName = embed.ProviderName;
            ProviderUrl = embed.ProviderUrl;
            ThumbnailUrl = embed.ThumbnailUrl;
            ThumbnailProxyUrl = embed.ThumbnailProxyUrl;
            ThumbnailHeight = embed.ThumbnailHeight;
            ThumbnailWidth = embed.ThumbnailWidth;
        }

        /// <summary> 
        /// Returns the User as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
