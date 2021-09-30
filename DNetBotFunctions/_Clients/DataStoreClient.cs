using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace DNetBotFunctions.Clients
{
    public class DataStoreClient
    {
        private CloudTableClient _tableClient;
        public DataStoreClient(CloudTableClient tableClient) { _tableClient = tableClient; }

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
