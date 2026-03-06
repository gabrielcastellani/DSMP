using DSMP.Application;
using Grpc.Net.Client;

Console.WriteLine("Collector is running...");

using var channel = GrpcChannel.ForAddress("https://localhost:7274");
var metricsPublisher = new Publisher.PublisherClient(channel);

while (true)
{
    var currentProcessors = Environment.ProcessorCount;

    Console.WriteLine($"[Date: {DateTime.UtcNow}] - Data sent to the server");

    await metricsPublisher.PushNumberOfProcessorsAsync(
        request: new PushMetricRequest
        {
            Process = currentProcessors
        });



    await Task.Delay(TimeSpan.FromSeconds(10));
}
