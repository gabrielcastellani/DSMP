using Grpc.Core;

namespace DSMP.Application.Services
{
    public sealed class MetricsPublisherService : Publisher.PublisherBase
    {
        private readonly ILogger<MetricsPublisherService> _logger;

        public MetricsPublisherService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MetricsPublisherService>();
        }

        public override Task<Reply> PushNumberOfProcessors(PushMetricRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Number of processors: {request.Process}");

            return Task.FromResult(new Reply());
        }
    }
}
