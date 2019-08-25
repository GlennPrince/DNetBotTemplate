using System;
using DNetUtils.Entities;
using DNetUtils.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace DNetBotFunctions
{
    public static class ProcessMessages
    {
        [FunctionName("InboundMessageProcess")]
        [return: ServiceBus("gamemasterbotmessagequeue", Connection = "AzureWebJobsServiceBus")]
        public static string InboundMessageProcess([QueueTrigger("discord-bot-inbound-queue")] CloudQueueMessage myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            ConvertedMessage message = DiscordConvert.DeSerializeObject(myQueueItem.AsString);

            if(message.Content.StartsWith("!ping"))
            {
                var returnMessage = new NewMessage();
                returnMessage.ChannelId = message.ChannelId;
                returnMessage.Content = "pong!";
                return JsonConvert.SerializeObject(returnMessage, Formatting.None);
            }
            return null;
        }
    }
}
