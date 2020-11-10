using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DNetUtils.Entities
{
    public class DiscordChannel
    {
        public ulong ID { get; set; }

        public DiscordChannel(SocketChannel channel)
        {
            ID = channel.Id;
        }

        public DiscordChannel(RestChannel channel)
        {
            ID = channel.Id;
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
