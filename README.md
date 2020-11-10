# DNetBot - A Discord Bot built using Dot Net Core
A Simple Discord Bot using the Discord .NET Bot library and Dot Net Core. This bot is built with a microservices architecture using a range of Azure Services such as Functions, Table Storage, Queue Storage etc. I'm trying to build this bot in a modular fashion with the basic functionality provide and documented in such a way as to be expandable by others.

For more information about the bot, and to follow it's progress, I am documenting it progress beginning with [a microservices discord bot - part 1](https://www.glennprince.com/blog/creating-a-discord-bot-part-01/). I have made some changes to the architecture [which you can review here](https://www.glennprince.com/blog/creating-a-discord-bot-part-04/), but the big one is replacing the eventing system with Azure Event Grid.

## Quick Start
You can use this library locally to some degree, however this bot requires Azure Event Grid to send and receive messages from Discord. To use this bot locally, in the DNetBot project create a Config\hostsettings.json file with the following settings:

```
{
  "environment": "Development",
  "LOG_LEVEL": "INFO",
  "DISCORD_BOT_TOKEN": "<Your Bot Token>",
  "EventGridDomainEndPoint": "<Event Grid Domain End Point>",
  "EventGridDomainAccessKey": "<Event Grid Domain Access Key>"
}

```

You can then build and run the application locally in development / debug mode. Currently the application will only respond to the !ping command with "pong!".

## Event Types

This Discord Bot is using EventGrid to send all Discord Events to any subscriber interested. It also listens to EventGrid to receive messages back for processing, currently the return message is the only value that is configured. Below is a list of all the event types, subjects and payloads sent by the proxy service. Payloads are defined in the entities section of the DNetUtils project.

Topic           |   Subject				|   Event Type                      |   Payload
client			|	ClientReady			|	DNetBot.Client.Ready			|	DiscordClient
bot				|	Update				|	DNetBot.Bot.Update				|	DiscordUser
bot				|	LoggedIn			|	DNetBot.Bot.LoggedIn			|	<None>
channel			|	ChannelCreated		|	DNetBot.Channel.Created			|	DiscordChannel
channel			|	ChannelUpdated		|	DNetBot.Channel.Updated			|	DiscordChannel
channel			|	ChannelDeleted		|	DNetBot.Channel.Deleted			|	DiscordChannel
channel			|	ChannelJoined		|	DNetBot.Channel.Joined			|	DiscordUser
channel			|	ChannelLeft			|	DNetBot.Channel.Left			|	DiscordUser
guild			|	JoinedGuild			|	DNetBot.Guild.Joined			|	DiscordGuild
guild			|	LeftGuild			|	DNetBot.Guild.Left				|	DiscordGuild
guild			|	GuildAvailable		|	DNetBot.Guild.Available			|	DiscordGuild
guild			|	UpdatedGuild		|	DNetBot.Guild.Updated			|	DiscordGuild
guild			|	GuildUnavailable	|	DNetBot.Guild.Unavailable		|	DiscordGuild
messages        |   NewMessage			|   DNetBot.Message.NewMessage      |   DiscordMessage
messages        |   DeletedMessage		|   DNetBot.Message.Deleted         |   DiscordMessage
messages        |   UpdatedMessage		|   DNetBot.Message.Updated         |   DiscordMessage
role			|	RoleCreated			|	DNetBot.Role.Created			|	DiscordRole
role			|	RoleUpdated			|	DNetBot.Role.Updated			|	DiscordRole
role			|	RoleDeleted			|	DNetBot.Role.Deleted			|	DiscordRole
user			|	UserBanned			|	DNetBot.User.Banned				|	DiscordUser
user			|	UserUnbanned		|	DNetBot.User.Unbanned			|	DiscordUser
user			|	UpdatedUser			|	DNetBot.User.Updated			|	DiscordUser
user			|	UpdatedMember		|	DNetBot.User.Updated			|	DiscordUser
user			|	UserJoined			|	DNetBot.User.Joined				|	DiscordUser
user			|	UserLeft			|	DNetBot.User.Left				|	DiscordUser