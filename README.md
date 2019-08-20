# DNetBot - A Discord Bot built using Dot Net Core
A Simple Discord Bot using the Discord .NET Bot library and Dot Net Core. This bot is built with a microservices architecture using Azure Services such as Functions, Table Storage, Queue Storage etc. For more information about the bot and to follow it's progress, see the blog post [A MICROSERVICES DISCORD BOT - PART 1](https://www.glennprince.com/blog/creating-a-discord-bot-part-01/) on my website.

# Use
Currently, to use this library and set the settings locally make sure you create a Config\hostsettings.json file formatted as below:

```
{
  "environment": "Development",
  "DISCORD_BOT_TOKEN": "YOUR_BOT_TOKEN"
}
```

and then build and run the application. Currently the application will only respond to the !ping command.