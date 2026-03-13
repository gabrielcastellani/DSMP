namespace DSMP.Collector.Protocols.Mqtt
{
    public sealed class MqttProtocolSettings : IProtocolSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
