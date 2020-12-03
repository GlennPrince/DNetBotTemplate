// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using DNetUtils.Entities;
using Microsoft.Azure.EventGrid;
using System.Collections.Generic;

namespace DNetBotFunctions.Messaging
{
    public static class NewMessage
    {
        private static EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));

        [FunctionName("NewMessage")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("New Message Event Triggered On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());
            DiscordMessage message = new DiscordMessage(eventGridEvent.Data.ToString());
            if (message.Content.StartsWith("!ping"))
            {
                var returnMessage = new DiscordMessage();
                returnMessage.ChannelId = message.ChannelId;
                returnMessage.Content = "pong!";        

                var myEvent = new EventGridEvent(eventGridEvent.Id, "ReturnMessage", returnMessage, "DNetBot.Message.ReturnMessage", DateTime.Now, "1.0", "returnmessage");
                string eventGridHostname = new Uri(System.Environment.GetEnvironmentVariable("EventGridDomain")).Host;
                try
                {
                    eventGridClient.PublishEventsAsync(eventGridHostname, new List<EventGridEvent>() { myEvent }).Wait();
                }
                catch(Exception ex)
                {
                    log.LogError(ex.ToString());
                }
            }
        }
    }
}
