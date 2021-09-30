using Microsoft.Extensions.Logging;

namespace DNetUtils
{
    public static class EventIdentifiers
    {
        public static readonly EventId DiscordGuildJoined = new EventId(1, "Discord Guild Joined");
    }
}
