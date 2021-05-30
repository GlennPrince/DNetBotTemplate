// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using DNetBotFunctions.Clients;
using DNetUtils.Entities;
using System.Collections.Generic;

namespace DNetBotFunctions.Events.Guild
{
    public static class GuildAvailable
    {
        private static EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));

        [FunctionName("GuildAvailable")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            if (eventGridEvent.Subject.Equals("GuildAvailable") && eventGridEvent.EventType.Equals("DNetBot.Guild.Available"))
            {
                log.LogInformation("Guild Joined Event Triggered On: {Topic} with the Subject: {Subject} and the ID: {GuildID}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString(), eventGridEvent.Data.ToString());

                var guild = RedisCacheClient.RetrieveGuild(eventGridEvent.Data.ToString());
                log.LogInformation("Raw Guild: " + RedisCacheClient.testGuild(eventGridEvent.Data.ToString()));

                log.LogInformation("Guild Info: " + guild.Name);
                
                var returnMessage = new DiscordMessage();
                returnMessage.ChannelId = guild.DefaultChannelId;
                returnMessage.Content = "I'm Back Baby !!";
                var myEvent = new EventGridEvent(eventGridEvent.Id, "ReturnMessage", returnMessage, "DNetBot.Message.ReturnMessage", DateTime.Now, "1.0", "returnmessage");
                string eventGridHostname = new Uri(System.Environment.GetEnvironmentVariable("EventGridDomain")).Host;
                try
                {
                    eventGridClient.PublishEventsAsync(eventGridHostname, new List<EventGridEvent>() { myEvent }).Wait();
                }
                catch (Exception ex)
                {
                    log.LogError(ex.ToString());
                }
            }
            else
            {
                // We could do something if the event is not the type we are expecting
            }
        }
    }
}
