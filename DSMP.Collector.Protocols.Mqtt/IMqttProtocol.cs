using DSMP.Collector.Protocols.Mqtt.Events;

namespace DSMP.Collector.Protocols.Mqtt
{
    public interface IMqttProtocol : IProtocol
    {
        event EventHandler<MqttMessageReceivedEventArgs> MessageReceived;
    }
}
