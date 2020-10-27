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

        public DiscordReaction(SocketReaction reaction)
        {
            MessageID = reaction.MessageId;
            UserID = reaction.UserId;
            ChannelId = reaction.Channel.Id;
            EmoteName = reaction.Emote.Name;
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
