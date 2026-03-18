namespace DSMP.Collector.Protocols.Mqtt
{
    public interface IMqttProtocolFactory
    {
        IMqttProtocol Create();
    }
}
