using Newtonsoft.Json;

namespace DNetUtils.Entities
{
    public class DiscordPermissionOverwrite
    {
        public ulong ID { get; set; }
        public int Type { get; set; }
        public string allow { get; set; }
        public string deny { get; set; }
    }
}
