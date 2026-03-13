namespace DSMP.Collector.Protocols.Events
{
    public sealed class ConnectionChangedEventArgs : EventArgs
    {
        public bool Connected { get; private set; }

        public ConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }
}
