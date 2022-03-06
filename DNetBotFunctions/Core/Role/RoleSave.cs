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

namespace DNetBotFunctions.Core.Role
{
    public class Core_Role_Save
    {
        private IConnectionMultiplexer _redis;
        private DataStoreClient _dataStore;
        public Core_Role_Save(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _dataStore = new DataStoreClient();
        }

        [FunctionName("Core_Role_Save")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(new EventId(2, "RoleSave"), "Core Role Save Service triggered from Role Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            if (eventGridEvent.EventType == "DNetBot.Role.Created" || eventGridEvent.EventType == "DNetBot.Role.Updated")
            {
                IDatabase cache = _redis.GetDatabase();
                var cachedRole = cache.StringGet(eventGridEvent.Data.ToString());
                var role = new DiscordRole(cachedRole);

                var storeRole = new RoleTableEntity(role.GuildID.ToString(), role.ID.ToString(), role);
                await _dataStore.InsertOrMergeObject("Roles", storeRole);

                foreach (var userID in role.MemberIDs)
                {
                    var cachedUser = cache.StringGet("role_members:" + role.ID.ToString() + ":" + userID.ToString());
                    var user = new DiscordUser(cachedUser.ToString());
                    var storeUser = new UserTableEntity(role.ID.ToString(), user.ID.ToString(), user);
                    await _dataStore.InsertOrMergeObject("RoleMembers", storeUser);
                }
            }
            else if (eventGridEvent.EventType == "DNetBot.Role.Deleted")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                _dataStore.DeleteObject("Roles", eventData[1]);
            }
        }
    }
}
