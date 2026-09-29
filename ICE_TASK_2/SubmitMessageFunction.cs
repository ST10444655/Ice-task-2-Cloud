using ICE_TASK_2.Models;
using ICE_TASK_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ICE_TASK_2;

public class SubmitMessageFunction
{
    private readonly ILogger<SubmitMessageFunction> _logger;
    private readonly QueueStorageService _queueStorage;

    public SubmitMessageFunction(ILogger<SubmitMessageFunction> logger, QueueStorageService queueStorage)
    {
        _logger = logger;
        _queueStorage = queueStorage;
    }

    [Function("SubmitMessage")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        //_logger.LogInformation("C# HTTP trigger function processed a request.");

        var body = await req.ReadFromJsonAsync<MessageRequest>();

        var messageRequest = new MessageRequest
        {
            Name = body.Name,
            MessageText = body.MessageText
        };

        string message = JsonSerializer.Serialize(messageRequest);
        await _queueStorage.SendMessageAsync(message);

        var response = req.CreateResponse(System.Net.HttpStatusCode.Created);
        await response.WriteAsJsonAsync(messageRequest);

        return response;
    }
}