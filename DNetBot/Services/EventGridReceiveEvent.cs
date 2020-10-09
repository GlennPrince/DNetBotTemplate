using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json.Linq;
using DNetUtils.Helpers;

namespace DNetBot.Services
{
    [Route("/api"), AllowAnonymous]
    public class WebHooksController : Controller
    {
        // POST: /api/event_grid_webhook
        [HttpPost("event_grid_webhook")]
        public IActionResult ProcessEvent([FromBody] EventGridEvent[] events)
        {
            foreach (var eventGridEvent in events)
            {
                // 1. If there is no EventType through a bad request
                if (eventGridEvent == null) return BadRequest();

                // 2. If the EventType is the Event Grid handshake event, respond with a SubscriptionValidationResponse.
                else if (eventGridEvent.EventType == EventTypes.EventGridSubscriptionValidationEvent)
                {
                    var data = (eventGridEvent.Data as JObject).ToObject<SubscriptionValidationEventData>();
                    var response = new SubscriptionValidationResponse(data.ValidationCode);
                    return Ok(response);
                }
                /*
                // 3. If the EventType is a return message, send a message to Discord
                else if (eventGridEvent.EventType == "ReturnMessage")
                {
                    var message = DeSerializeObject(eventGridEvent.Data);
                    SendMessage(message);
                    return Ok();
                }*/
                else
                    return BadRequest();
            }
            return Ok();
        }
    }
}
