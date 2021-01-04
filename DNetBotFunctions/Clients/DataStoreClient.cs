using DNetBotFunctions.Clients.TableEntities;
using DNetUtils.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNetBotFunctions.Clients
{
    public class DataStoreClient
    {
        private static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"));

        public static async void InsertGuild(DiscordGuild guild)
        {
            var guildTable = await CreateTableAsync("Guilds");

            var guildEntity = new GuildEntity(guild);
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(guildEntity);
            await guildTable.ExecuteAsync(insertOrMergeOperation);
        }

        public static async void InsertChannel(ulong guildID, ulong channelID)
        {
            var guildTable = await CreateTableAsync("Guilds");

            var channelEntity = new ChannelEntity(guildID, channelID);
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(channelEntity);
            await guildTable.ExecuteAsync(insertOrMergeOperation);
        }

        private static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
