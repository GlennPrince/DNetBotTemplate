// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=Core_GuildCreate
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using StackExchange.Redis;
using DNetUtils.Entities;
using DNetBotFunctions.Clients;
using DNetBotFunctions.Core.Data;

namespace DNetBotFunctions.Core.Channel
{
    public class Core_Channel_Save
    {
        private IConnectionMultiplexer _redis;
        private DataStoreClient _dataStore;
        public Core_Channel_Save(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _dataStore = new DataStoreClient();
        }

        [FunctionName("Core_Channel_Save")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(new EventId(2, "ChannelSave"), "Core Channel Save Service triggered from Channel Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            if (eventGridEvent.EventType == "DNetBot.Channel.Created" || eventGridEvent.EventType == "DNetBot.Channel.Updated")
            {
                IDatabase cache = _redis.GetDatabase();
                var cachedChannel = cache.StringGet(eventGridEvent.Data.ToString());
                var channel = new DiscordChannel(cachedChannel);

                var storeChannel = new ChannelTableEntity(channel.GuildID.ToString(), channel.ID.ToString(), channel);
                await _dataStore.InsertOrMergeObject("Channels", storeChannel);

                foreach (var userID in channel.Users)
                {
                    var cachedUser = cache.StringGet("channel_users:" + channel.ID.ToString() + ":" + userID.ToString());
                    var user = new DiscordEmote(cachedUser.ToString());
                    var storeUser = new EmoteTableEntity(user.ID.ToString(), channel.ID.ToString(), user);
                    await _dataStore.InsertOrMergeObject("Users", storeUser);
                }
            }
            else if (eventGridEvent.EventType == "DNetBot.Channel.Deleted")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                await _dataStore.DeleteObject("Channels", eventData[1], eventData[2]);
            }
            else if (eventGridEvent.EventType == "DNetBot.Channel.Joined")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                IDatabase cache = _redis.GetDatabase();

                var cachedUser = cache.StringGet("channel_users:" + eventData[2] + ":" + eventData[1]);
                var user = new DiscordEmote(cachedUser.ToString());
                var storeUser = new EmoteTableEntity(eventData[2], eventData[1], user);
                await _dataStore.InsertOrMergeObject("Users", storeUser);
            }
            else if (eventGridEvent.EventType == "DNetBot.Channel.Left")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                await _dataStore.DeleteObject("Users", eventData[2], eventData[1]);
            }
        }
    }
}
