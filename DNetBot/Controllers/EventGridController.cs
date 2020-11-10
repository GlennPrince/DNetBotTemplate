using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNetBot.Services;
using DNetUtils.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DNetBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventGridController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly DiscordSocketService _discordSocketService;

        public EventGridController(ILogger<string> logger, DiscordSocketService discordSocketService)
        {
            _logger = logger;
            _discordSocketService = discordSocketService;
        }

        // POST: /api/EventGrid/ReturnMessage
        [HttpPost("returnmessage")]
        public IActionResult ReturnMessage([FromBody] EventGridEvent[] events)
        {
            foreach (var eventGridEvent in events)
            {
                _logger.Log(LogLevel.Information, "Event Grid Event Received. Type: " + eventGridEvent.EventType.ToString());

                // 1. If there is no EventType through a bad request
                if (eventGridEvent == null) return BadRequest();

                // 2. If the EventType is the Event Grid handshake event, respond with a SubscriptionValidationResponse.
                else if (eventGridEvent.EventType == EventTypes.EventGridSubscriptionValidationEvent)
                    return Ok(ValidateWebHook(eventGridEvent.Data));

                // 3. If the EventType is a return message, send a message to Discord
                else if (eventGridEvent.EventType == "DNetBot.Message.ReturnMessage")
                {
                    var message = JsonConvert.DeserializeObject<DiscordMessage>(eventGridEvent.Data.ToString());
                    _discordSocketService.SendMessage(message).Wait();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            return Ok();
        }

        private SubscriptionValidationResponse ValidateWebHook(object eventGridData)
        {
            var payload = JsonConvert.DeserializeObject<SubscriptionValidationEventData>(eventGridData.ToString());
            return new SubscriptionValidationResponse(payload.ValidationCode);
        }
    }
}
