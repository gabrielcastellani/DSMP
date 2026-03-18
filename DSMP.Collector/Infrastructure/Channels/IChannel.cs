using DSMP.Collector.Infrastructure.Channels.Events;

namespace DSMP.Collector.Infrastructure.Channels
{
    public interface IChannel : IAsyncDisposable
    {
        bool Connected { get; }

        event EventHandler<ChannelConnectionChangedEventArgs> ConnectionChanged;
        event EventHandler<ChannelMessageReceivedEventArgs> MessageReceived;

        Task Initialize();
    }
}
