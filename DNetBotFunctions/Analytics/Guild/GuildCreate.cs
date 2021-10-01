
// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=Analytics_GuildCreate
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using StackExchange.Redis;
using DNetUtils;
using Newtonsoft.Json;
using DNetUtils.Entities;
using DNetBotFunctions.Clients;
using DNetBotFunctions.Analytics.Data;

namespace DNetBotFunctions.Analytics.Guild
{
    public class Analytics_GuildCreate
    {
        private EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));

        private IConnectionMultiplexer _redis;
        private DataStoreClient _dataStore;
        public Analytics_GuildCreate(IConnectionMultiplexer redis) 
        { 
            _redis = redis;
            _dataStore = new DataStoreClient();
        }

        [FunctionName("Analytics_GuildCreate")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(new EventId(1, "GuildJoin"), "Analytics Guild Create Service triggered from Guild Joined Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            IDatabase cache = _redis.GetDatabase();
            var cachedGuild = cache.StringGet(eventGridEvent.Data.ToString());

            var guild = JsonConvert.DeserializeObject<DiscordGuild>(cachedGuild);
                
            var storeGuild = new GuildTableEntity(guild.Id.ToString(), "Guild.Info", guild);
            _dataStore.InsertOrMergeObject("AnalyticsGuilds", storeGuild);

            foreach (var channelID in guild.ChannelIds)
            {
                var cachedChannel = cache.StringGet("channel:" + channelID.ToString());
                var channel = JsonConvert.DeserializeObject<DiscordChannel>(cachedChannel);
                var storeChannel = new ChannelTableEntity(guild.Id.ToString(), channel.ToString(), channel);
                _dataStore.InsertOrMergeObject("AnalyticsChannels", storeChannel);

                if(guild.CategoryIds.Contains(channelID))
                    _dataStore.InsertOrMergeObject("AnalyticsCategoryChannels", storeChannel);

                if (guild.VoiceIds.Contains(channelID))
                    _dataStore.InsertOrMergeObject("AnalyticsVoiceChannels", storeChannel);
            }

            foreach (var roleID in guild.RoleIds)
            {
                var cachedRole = cache.StringGet("role:" + guild.Id.ToString() + ":" + roleID.ToString());
                var role = JsonConvert.DeserializeObject<DiscordRole>(cachedRole);
                var storeRole = new RoleTableEntity(guild.Id.ToString(), role.ToString(), role);
                _dataStore.InsertOrMergeObject("AnalyticsRoles", storeRole);
            }

            foreach (var emoteID in guild.EmoteIds)
            {
                var cachedEmote = cache.StringGet("emote:" + guild.Id.ToString() + ":" + emoteID.ToString());
                var emote = JsonConvert.DeserializeObject<DiscordEmote>(cachedEmote);
                var storeEmote = new EmoteTableEntity(guild.Id.ToString(), emoteID.ToString(), emote);
                _dataStore.InsertOrMergeObject("AnalyticsEmotes", storeEmote);
            }
        }
    }
}
