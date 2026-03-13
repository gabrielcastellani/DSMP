using DSMP.Collector.Protocols.Events;

namespace DSMP.Collector.Protocols
{
    public interface IProtocol : IAsyncDisposable
    {
        event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;

        Task Initialize(IProtocolSettings settings);
    }
}
