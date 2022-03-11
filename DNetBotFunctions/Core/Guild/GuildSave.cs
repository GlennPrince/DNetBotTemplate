// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=Core_GuildCreate
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using StackExchange.Redis;
using DNetUtils.Entities;
using DNetBotFunctions.Clients;
using DNetBotFunctions.Core.Data;
using Discord;

namespace DNetBotFunctions.Core.Guild
{
    public class Core_Guild_Save
    {
        private EventGridClient eventGridClient = new EventGridClient(new TopicCredentials(System.Environment.GetEnvironmentVariable("EventGridKey")));

        private IConnectionMultiplexer _redis;
        private DataStoreClient _dataStore;
        public Core_Guild_Save(IConnectionMultiplexer redis) 
        { 
            _redis = redis;
            _dataStore = new DataStoreClient();
        }
 
        [FunctionName("Core_Guild_Save")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(new EventId(1, "GuildSave"), "Core Guild Save Service triggered from Guild Event On: {Topic} with the Subject: {Subject}", eventGridEvent.Topic.ToString(), eventGridEvent.Subject.ToString());

            if (eventGridEvent.EventType == "DNetBot.Guild.Joined" || eventGridEvent.EventType == "DNetBot.Guild.Available" || eventGridEvent.EventType == "DNetBot.Guild.Updated")
            {
                IDatabase cache = _redis.GetDatabase();
                var cachedGuild = cache.StringGet(eventGridEvent.Data.ToString());
                var guild = new DiscordGuild(cachedGuild);

                var storeGuild = new GuildTableEntity(guild.Id.ToString(), "Guild.Info", guild);
                await _dataStore.InsertOrMergeObject("Guilds", storeGuild);

                foreach (var channelID in guild.CategoryIds)
                {
                    var cachedChannel = cache.StringGet("channel:" + channelID.ToString());
                    var channel = new DiscordChannel(cachedChannel.ToString());
                    var storeChannel = new ChannelTableEntity(guild.Id.ToString(), channel.ID.ToString(), channel);
                    await _dataStore.InsertOrMergeObject("Channels", storeChannel);

                    if (guild.CategoryIds.Contains(channelID))
                        await _dataStore.InsertOrMergeObject("CategoryChannels", storeChannel);

                    if (guild.VoiceIds.Contains(channelID))
                        await _dataStore.InsertOrMergeObject("VoiceChannels", storeChannel);
                    
                    if(channel.ChannelType == ChannelType.Text)
                        await _dataStore.InsertOrMergeObject("TextChannels", storeChannel);
                }

                foreach (var userID in guild.UserIds)
                {
                    var cachedUser = cache.StringGet("user:" + guild.Id.ToString() + ":" + userID.ToString());
                    var user = new DiscordUser(cachedUser.ToString());
                    var storeUser = new UserTableEntity(guild.Id.ToString(), user.ID.ToString(), user);
                    await _dataStore.InsertOrMergeObject("Users", storeUser);
                }

                foreach (var roleID in guild.RoleIds)
                {
                    var cachedRole = cache.StringGet("role:" + guild.Id.ToString() + ":" + roleID.ToString());
                    var role = new DiscordRole(cachedRole.ToString());
                    var storeRole = new RoleTableEntity(guild.Id.ToString(), role.ID.ToString(), role);
                    await _dataStore.InsertOrMergeObject("Roles", storeRole);
                }

                foreach (var emoteID in guild.EmoteIds)
                {
                    var cachedEmote = cache.StringGet("emote:" + guild.Id.ToString() + ":" + emoteID.ToString());
                    var emote = new DiscordEmote(cachedEmote.ToString());
                    var storeEmote = new EmoteTableEntity(guild.Id.ToString(), emote.ID.ToString(), emote);
                    await _dataStore.InsertOrMergeObject("Emotes", storeEmote);
                }

                foreach (var stickerID in guild.StickerIds)
                {
                    var cachedSticker = cache.StringGet("sticker:" + stickerID.ToString());
                    var sticker = new DiscordSticker(cachedSticker.ToString());
                    var storesticker = new StickerTableEntity(sticker.GuildID.ToString(), stickerID.ToString(), sticker);
                    await _dataStore.InsertOrMergeObject("Stickers", storesticker);
                }
            }
            else if (eventGridEvent.EventType == "DNetBot.Guild.Left")
            {
                string[] eventData = eventGridEvent.Data.ToString().Split(':');
                await _dataStore.DeleteObject("Guilds", eventData[1], "Guild.Info");

                _dataStore.DeleteObject("Channels", eventData[1]);
                _dataStore.DeleteObject("CategoryChannels", eventData[1]);
                _dataStore.DeleteObject("VoiceChannels", eventData[1]);
                _dataStore.DeleteObject("TextChannels", eventData[1]);
                _dataStore.DeleteObject("Users", eventData[1]);
                _dataStore.DeleteObject("Roles", eventData[1]);
                _dataStore.DeleteObject("Emotes", eventData[1]);
                _dataStore.DeleteObject("Messages", eventData[1]);
            }
        }
    }
}
