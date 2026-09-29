using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using ICE_TASK_2.Models;
using ICE_TASK_2.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ICE_TASK_2;

public class ProcessMessageFunction
{
    private readonly ILogger<ProcessMessageFunction> _logger;
    private readonly TableStorageService _tableService;

    public ProcessMessageFunction(ILogger<ProcessMessageFunction> logger, TableStorageService tableService)
    {
        _logger = logger;
        _tableService = tableService;
    }

    [Function("ProcessMessage")]
    public async Task Run([QueueTrigger("message-queue", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        //_logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);

        var messageRequest = JsonSerializer.Deserialize<MessageRequest>(message.Body.ToString());

        var messageEntity = new MessageEntity
        {
            Name = messageRequest.Name,
            MessageText = messageRequest.MessageText
        };

        await _tableService.AddMessageAsync(messageEntity);
    }
}