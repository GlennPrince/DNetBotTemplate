// Default URL for triggering event grid function in the local environment.
// https://4fb77cabbcf2.ngrok.io/runtime/webhooks/EventGrid?functionName=GuildJoin
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using DNetUtils.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using DNetBotFunctions.Clients;
using StackExchange.Redis;

namespace DNetBotFunctions.Events.Guild
{
    public class GuildJoin
    {
        private EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));
        private IConnectionMultiplexer _redis;
        public GuildJoin(IConnectionMultiplexer redis) { _redis = redis; }

        [FunctionName("GuildJoin")]
        public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            if (eventGridEvent.Subject.Equals("JoinedGuild") && eventGridEvent.EventType.Equals("DNetBot.Guild.Joined"))
            {
                log.LogInformation("Guild Joined Event Triggered On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

                var guild = JsonConvert.DeserializeObject<DiscordGuild>(eventGridEvent.Data.ToString());

                DataStoreClient.InsertGuild(guild);

                foreach (var channel in guild.ChannelIds)
                    DataStoreClient.InsertChannel(guild.Id, channel);

                if (guild.DefaultChannelId != 0)
                {
                    var returnMessage = new DiscordMessage();
                    returnMessage.ChannelId = guild.DefaultChannelId;
                    returnMessage.Content = "The Discord Net Bot has entered the building";
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
            }
            else
            {
                // We could do something if the event is not the type we are expecting
            }
        }
    }
}
