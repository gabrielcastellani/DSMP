using MQTTnet;

namespace DSMP.Collector.Protocols.Mqtt
{
    internal sealed class MqttProtocolFactory : IMqttProtocolFactory
    {
        private readonly MqttClientFactory _mqttClientFactory;

        public MqttProtocolFactory()
        {
            _mqttClientFactory = new MqttClientFactory();
        }

        public IMqttProtocol Create()
        {
            return new MqttProtocol(_mqttClientFactory);
        }
    }
}
