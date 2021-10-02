using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
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

        public async Task<TableResult> RetrieveObject(string tableName, string partitionKey, string rowKey)
        {
            var table = await RetrieveTableAsync(tableName);

            TableOperation retrieveObjectOperation = TableOperation.Retrieve(partitionKey, rowKey);
            var result = await table.ExecuteAsync(retrieveObjectOperation);
            return result;
        }

        public async Task<IEnumerable<TableEntity>> RetrieveObject(string tableName, string partitionKey)
        {
            var table = await RetrieveTableAsync(tableName);
            
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
            table.ExecuteQuery(rangeQuery);
            var entities = rangeQuery.Execute();

            return entities;
        }

        public async Task<TableResult> DeleteObject(string tableName, string partitionKey, string rowKey)
        {
            var table = await RetrieveTableAsync(tableName);

            var entity = TableOperation.Retrieve(partitionKey, rowKey).Entity;
            TableOperation deleteEntity = TableOperation.Delete(entity);
            var result = await table.ExecuteAsync(deleteEntity);
            return result;
        }

        public async void DeleteObject(string tableName, string partitionKey)
        {
            var table = await RetrieveTableAsync(tableName);

            TableQuery<ITableEntity> rangeQuery = new TableQuery<ITableEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey",QueryComparisons.Equal, partitionKey));
            var entities = rangeQuery.Execute();

            TableBatchOperation ops = new TableBatchOperation();

            foreach (var entity in entities)
            {
                TableOperation deleteEntity = TableOperation.Delete(entity);
                ops.Add(deleteEntity);
                if(ops.Count > 90)
                {
                    table.ExecuteBatch(ops);
                    ops.Clear();
                }
            }
        }
    }
}
