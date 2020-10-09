using Discord;
using DNetBot.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        private Task SendEvent(string topic, string subject, string eventType, string data)
        {
            var eventsList = new List<EventGridEvent>();
            eventsList.Add(new EventGridEvent()
            {
                Topic = topic,
                Id = Guid.NewGuid().ToString(),
                EventType = eventType,
                Data = data,
                EventTime = DateTime.Now,
                Subject = subject,
                DataVersion = "1.0"
            });

            try
            {
                eventGridClient.PublishEventsAsync(eventGridDomainHostname, eventsList).Wait();
            }
            catch (Exception ex)
            {
                Formatter.GenerateLog(_logger, LogSeverity.Error, "EventGrid", "Unable to add new message to EventGrid. Error: "
                    + ex.Message + " | Inner: " + ex.InnerException.Message);
            }

            return Task.CompletedTask;
        }
    }
}
