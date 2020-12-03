# DNetBot - A Discord Bot built using Dot Net Core
A Simple Discord Bot using the Discord .NET Bot library and Dot Net Core. This bot is built with a microservices architecture using a range of Azure Services such as Functions and Event Grid. I'm trying to build this bot in a modular fashion with the basic functionality provide and documented in such a way as to be expandable by others.

## Quick Start
This bot now uses GitHub actions to build and deploy the infrastructure and code to Microsoft Azure. To get the bot up and running for you as quickly as possible you will need to:

1) Ensure you have an Azure Tenancy with a Resource Group setup and ready to go, as well as [Discord Developer Account](https://discord.com/developers/docs/intro) with an application and bot token.
2) Create a Service Principle with access to this Resource Group and save the corresponding JSON output. You do this with the [Azure Command Line](https://docs.microsoft.com/en-us/cli/azure) or the [Cloud Shell](https://docs.microsoft.com/en-us/azure/cloud-shell/overview) with the following command line:
	- `az ad sp create-for-rbac --name "github-deployer" --sdk-auth --role contributor --scopes /subscriptions/<YOUR_SUBSCRIPTION_ID>/resourcegroups/<YOUR_RESOURCE_GROUP>`
3) Fork the main branch or releases of this repository into your GitHub own account
4) Add the following secrets to your repository:
    - `AZURE_CREDENTIALS` with the JSON from step 1
    - `SUBSCRIPTION_ID` with your Azure Subscription ID
    - `DISCORD_BOT_TOKEN` with your Bot token from the Discord Developer Portal
5) Modify the environment variables at the top of the `deployment.yml` file to suit your needs / customize the naming of resources:
```
env:
    DNETBOT_NAME: 'discordnetbot'   # DNet Bot Naming Prefix for Resources (Lowercase, No Spaces or Special Characters)
    DOTNET_VERSION: '3.1.403'       # SDK Version to use
    AZURE_LOCATION: 'eastus'        # Resource Location
    AZURE_RG: 'RG-DNet-Bot'         # Resource Group Name to use
    AZURE_WEB_SKU: 'B1'             # Size of the WebApp to deploy
```
6) Create a new release or manually trigger the workflow to deploy the bot to your Azure instance. 
7) Currently the application will only respond to the !ping command with "pong!".

To extend this bot and add your own event handlers there is [documentation in the wiki](https://github.com/GlennPrince/DNetBotTemplate/wiki/Building-an-Event-Handler).

## Project Documentation

- [The wiki](https://github.com/GlennPrince/DNetBotTemplate/wiki) contains all the relevant documentation about using and expanding upon the Discord Bot to implement your own functionality. 
- [The change log](https://github.com/GlennPrince/DNetBotTemplate/blob/master/CHANGELOG.md) has both the history of changes and current changes in development
- [My website](https://www.glennprince.com/categories/bots/) has a number of posts about the development and background of this bot template.