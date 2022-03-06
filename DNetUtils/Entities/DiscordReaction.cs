using Discord.WebSocket;
using Newtonsoft.Json;

namespace DNetUtils.Entities
{
    public class DiscordReaction
    {
        public ulong MessageID { get; set; }
        public ulong UserID { get; set; }
        public ulong ChannelId { get; set; }
        public string EmoteName { get; set; }

        public DiscordReaction() { }

        public DiscordReaction(SocketReaction reaction)
        {
            MessageID = reaction.MessageId;
            UserID = reaction.UserId;
            ChannelId = reaction.Channel.Id;
            EmoteName = reaction.Emote.Name;
        }

        public DiscordReaction(string json)
        {
            var reaction = JsonConvert.DeserializeObject<DiscordReaction>(json);

            MessageID = reaction.MessageID;
            UserID = reaction.UserID;
            ChannelId = reaction.ChannelId;
            EmoteName = reaction.EmoteName;
        }

        /// <summary> 
        /// Returns the Reaction as a JSON formatted string
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
