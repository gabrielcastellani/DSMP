namespace DSMP.Collector.Domain.Channels.Aggregates
{
    public class ChannelSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Topic { get; set; }
    }
}
