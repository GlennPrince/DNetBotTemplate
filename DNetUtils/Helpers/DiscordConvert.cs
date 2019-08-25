using DNetUtils.Entities;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DNetUtils.Helpers
{
    public static class DiscordConvert
    {
        public static string SerializeObject(SocketMessage message)
        {
            var converted = new ConvertedMessage(message);
            return JsonConvert.SerializeObject(converted, Formatting.None);
        }

        public static ConvertedMessage DeSerializeObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<ConvertedMessage>(jsonString);
        }
    }
}
