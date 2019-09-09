using System;
using DNetUtils.Entities;
using DNetUtils.Helpers;
using DNetUtils.TableEntities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace DNetBotFunctions
{
    public static class ProcessActivity
    {
        [FunctionName("InboundActivityProcess")]
        [return: Table("ActivityTable")]
        public static ReceivedMessage InboundActivityProcess([QueueTrigger("discord-activity-inbound-queue")] CloudQueueMessage myQueueItem, ILogger log)
        {
            log.LogInformation($"Activity Queue trigger function processed: {myQueueItem}");

            ConvertedMessage message = DiscordConvert.DeSerializeObject(myQueueItem.AsString);
            return new ReceivedMessage(message);
        }
    }
}
