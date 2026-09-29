using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICE_TASK_2.Models
{
    public class MessageEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Message";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string? Name { get; set; }
        public string? MessageText { get; set; }
    }
}
