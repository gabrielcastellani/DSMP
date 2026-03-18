namespace DSMP.Collector.Infrastructure.Channels.Events
{
    public sealed class ChannelConnectionChangedEventArgs : EventArgs
    {
        public bool Connected { get; private set; }

        public ChannelConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }
}
