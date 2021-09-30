using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace DNetBotFunctions.Clients
{
    public class DataStoreClient
    {
        private CloudTableClient _tableClient;
        CloudStorageAccount _storageAccount;

        public DataStoreClient() 
        {
            _storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration()); 
        }

        public async Task<CloudTable> RetrieveTableAsync(string tableName)
        {
            // Create a table client for interacting with the table service 
            CloudTable table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }

        public async Task<TableResult> InsertOrMergeObject(string tableName, ITableEntity entity)
        {
            var table = await RetrieveTableAsync(tableName);

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
            var result = await table.ExecuteAsync(insertOrMergeOperation);
            return result;
        }

        public async Task<ITableEntity> RetrieveObject(string tableName, string partitionKey, string rowKey)
        {
            var table = await RetrieveTableAsync(tableName);

            TableOperation retrieveObjectOperation = TableOperation.Retrieve(partitionKey, rowKey);
            return retrieveObjectOperation.Entity;
        }
    }
}
