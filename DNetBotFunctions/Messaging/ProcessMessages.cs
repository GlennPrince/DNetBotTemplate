using System;
using DNetUtils.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DNetBotFunctions
{
    /*
    public static class ProcessMessages
    {
        [FunctionName("InboundMessageProcess")]
        [return: ServiceBus("dnetbotmessagequeue", Connection = "AzureWebJobsServiceBus")]
        public static string InboundMessageProcess([QueueTrigger("discord-messages-inbound-queue")] CloudQueueMessage myQueueItem, ILogger log)
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
    */
}
