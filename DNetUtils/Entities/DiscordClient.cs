using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DNetUtils.Entities
{
    public class DiscordClient
    {
        public ConnectionState connectionState;
        public ulong botID;
        public int latency;
        public ICollection<ulong> dmChannels;
        public ICollection<ulong> guildChannels;

        public DiscordClient(DiscordSocketClient client)
        {
            connectionState = client.ConnectionState;
            botID = client.CurrentUser.Id;
            latency = client.Latency;

            dmChannels = new List<ulong>();
            foreach (var channels in client.DMChannels)
                dmChannels.Add(channels.Id);

            guildChannels = new List<ulong>();
            foreach (var guild in client.Guilds)
                guildChannels.Add(guild.Id);
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
