using Azure.Data.Tables;
using Azure.Monitor.OpenTelemetry.Exporter;
using ICE_TASK_2.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;

var builder = FunctionsApplication.CreateBuilder(args);

var connectionString = builder.Configuration["AzureWebJobsStorage"];

builder.Services.AddSingleton(options => 
    new QueueStorageService(connectionString, "message-queue"));

builder.Services.AddSingleton(options =>
    new TableStorageService(connectionString, "messageTable"));

builder.ConfigureFunctionsWebApplication();

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter();
}

builder.Build().Run();
