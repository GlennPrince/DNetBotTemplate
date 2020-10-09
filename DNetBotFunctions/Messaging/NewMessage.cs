// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using DNetUtils.Entities;
using DNetUtils.Helpers;

namespace DNetBotFunctions.Messaging
{
    public static class NewMessage
    {
        [FunctionName("NewMessage")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {

            log.LogInformation("New Message Event Triggered On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            ConvertedMessage message = DiscordConvert.DeSerializeObject(eventGridEvent.Data.ToString());

            if (message.Content.StartsWith("!ping"))
            {
                var returnMessage = new ConvertedMessage();
                returnMessage.ChannelId = message.ChannelId;
                returnMessage.Content = "pong!";
            }
        }
    }
}
