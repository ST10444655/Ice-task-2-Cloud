using Azure.Data.Tables;
using ICE_TASK_2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICE_TASK_2.Services
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(string connectionString, string tableName)
        {
            var tableServiceClient = new TableServiceClient(connectionString);
            _tableClient = tableServiceClient.GetTableClient(tableName);

            _tableClient.CreateIfNotExists();
        }

        public async Task AddMessageAsync(MessageEntity entity)
        {
            await _tableClient.AddEntityAsync(entity);
        }
    }
}
