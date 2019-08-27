# DNetBot - A Discord Bot built using Dot Net Core
A Simple Discord Bot using the Discord .NET Bot library and Dot Net Core. This bot is built with a microservices architecture using a range of Azure Services such as Functions, Table Storage, Queue Storage etc. I'm trying to build this bot in a modular fashion with the basic functionality provide and documented in such a way as to be expandable by others.

For more information about the bot, and to follow it's progress, I am documenting it progress beginning with [a microservices discord bot - part 1](https://www.glennprince.com/blog/creating-a-discord-bot-part-01/).

# Quick Start
You can use this library locally to some degree, however this bot requires Azure Service Bus to send messages back to Discord. 

To use this bot locally, in the DNetBot project create a Config\hostsettings.json file with the following settings:

```
{
  "environment": "Development",
  "DISCORD_BOT_TOKEN": "<YOUR BOT TOKEN>",
  "StorageQueueConnectionString": "UseDevelopmentStorage=true",
  "AzureWebJobsServiceBus": "<YOUR AZURE SERVICE BUS CONNECTION STRING>"
}

```
You also need create a Config\local.settings.json file with the following settings:

```
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "InboundMessageQueue": "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;",
    "AzureWebJobsServiceBus": "<YOUR BOT TOKEN>"
  }
}
```

You can then build and run the application locally in development / debug mode. Currently the application will only respond to the !ping command with pong!.