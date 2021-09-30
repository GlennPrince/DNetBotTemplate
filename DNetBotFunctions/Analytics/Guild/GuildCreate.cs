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
            if (eventGridEvent.Subject.Equals("JoinedGuild") && eventGridEvent.EventType.Equals("DNetBot.Guild.Joined"))
            {
                log.LogInformation(new EventId(1, "GuildJoin"), "Analytics Guild Create Service triggered from Guild Joined Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());
                var guild = JsonConvert.DeserializeObject<DiscordGuild>(eventGridEvent.Data.ToString());
                
                var storeGuild = new GuildTableEntity(guild.Id.ToString(), "Guild.Info", guild);
                _dataStore.InsertOrMergeObject("AnalyticsGuilds", storeGuild);

                foreach (var channel in guild.ChannelIds)
                {
                    var storeChannel = new ChannelTableEntity(guild.Id.ToString(), channel.ToString(), guild.Id, channel);
                    _dataStore.InsertOrMergeObject("AnalyticsChannels", storeChannel);
                }
            }
            else
            {
                // We could do something if the event is not the type we are expecting
            }
        }
    }
}
