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

namespace DNetBotFunctions.Core.Guild
{
    public class Core_Message_Save
    {
        private EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));

        private IConnectionMultiplexer _redis;
        private DataStoreClient _dataStore;
        public Core_Message_Save(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _dataStore = new DataStoreClient();
        }

        [FunctionName("Core_Message_Save")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(new EventId(3, "Message"), "Core Message Save Service triggered from Channel Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            if (eventGridEvent.EventType == "DNetBot.Message.NewMessage" || eventGridEvent.EventType == "DNetBot.Message.Updated")
            {
                IDatabase cache = _redis.GetDatabase();
                var cachedMessage = cache.StringGet(eventGridEvent.Data.ToString());
                var message = new DiscordMessage(cachedMessage);

                var storeMessage = new MessageTableEntity(message.ChannelId.ToString(), message.MessageId.ToString(), message);
                _dataStore.InsertOrMergeObject("Messages", storeMessage);

                foreach (var attachmentID in message.AttachmentIDs)
                {
                    var cachedAttachment = cache.StringGet("message_attachment:" + message.MessageId.ToString() + ":" + attachmentID.ToString());
                    var attachment = new DiscordAttachment(cachedAttachment.ToString());
                    var storeAttachment = new AttachmentTableEntity(message.MessageId.ToString(), attachment.ID.ToString(), attachment);
                    _dataStore.InsertOrMergeObject("Attachments", storeAttachment);
                }
            }
            else if (eventGridEvent.EventType == "DNetBot.Message.Deleted")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                _dataStore.DeleteObject("Messages", eventData[1], eventData[2]);
                _dataStore.DeleteObject("Attachments", eventData[2]);
            }  
        }
    }
}
