using DNetUtils.Entities;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNetBotFunctions.Clients
{
    public class RedisCacheClient
    {
        private static Lazy<ConnectionMultiplexer> redisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = System.Environment.GetEnvironmentVariable("RedisServer").ToString();
            //var connection = ConnectionMultiplexer.Connect(cacheConnection.ToString());
            return ConnectionMultiplexer.Connect("localhost:6379,password=mylocalredispassword,ssl=false");
        }, LazyThreadSafetyMode.PublicationOnly);

        private static ConnectionMultiplexer Connection
        {
            get
            {
                return redisConnection.Value;
            }
        }

        public static DiscordGuild RetrieveGuild(string guildId)
        {
            var cachedGuild = (string)Connection.GetDatabase().StringGet(string.Format("guild:{0}", guildId));
            if (string.IsNullOrEmpty(cachedGuild))
                return new DiscordGuild();
            else
            {
                var guild = new DiscordGuild(cachedGuild);
                return guild;
            }
        }

        public static string testGuild(string guildId)
        {
            var cachedGuild = (string)Connection.GetDatabase().StringGet(string.Format("guild:{0}", guildId));
            if (string.IsNullOrEmpty(cachedGuild))
                return "";
            else
            {
                return cachedGuild;
            }
        }
    }
}
