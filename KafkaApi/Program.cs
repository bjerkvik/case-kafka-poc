using Confluent.Kafka;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "localhost:9094"
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

var app = builder.Build();

app.MapPost("/events", async (HttpRequest request, IProducer<Null, string> producer) =>
{
    string body;
    using (var reader = new StreamReader(request.Body))
    {
        body = await reader.ReadToEndAsync();
    }

    if (string.IsNullOrEmpty(body))
    {
        return Results.BadRequest("Request body cannot be empty.");
    }

    try
    {
        JsonDocument.Parse(body);
    }
    catch (JsonException ex)
    {
        return Results.BadRequest($"Invalid JSON format: {ex.Message}");
    }

    try
    {
        var message = new Message<Null, string> { Value = body };
        var result = await producer.ProduceAsync("events", message);
        return Results.Ok(new 
        {
            status = "published",
            topic = result.Topic,
            partition = result.Partition.Value,
            offset = result.Offset.Value,
        });
    }
    catch (ProduceException<Null, string> ex)
    {
        Console.Error.WriteLine($"Failed to publish message: {ex.Error.Reason}");
        return Results.Problem(
            title: "Failed to publish message",
            detail: ex.Error.Reason,
            statusCode: 503
        );
    }
});

app.MapGet("/health", () => Results.Ok("Kafka API is healthy."));

app.Run();
