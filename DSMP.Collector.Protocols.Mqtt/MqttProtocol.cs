using DSMP.Collector.Protocols.Events;
using DSMP.Collector.Protocols.Mqtt.Events;
using MQTTnet;
using MQTTnet.Formatter;
using System.Buffers;

namespace DSMP.Collector.Protocols.Mqtt
{
    internal sealed class MqttProtocol : IMqttProtocol
    {
        private readonly MqttClientFactory _mqttClientFactory;

        private IMqttClient? _mqttClient;
        private MqttProtocolSettings? _protocolSettings;

        public event EventHandler<MqttMessageReceivedEventArgs>? MessageReceived;
        public event EventHandler<ConnectionChangedEventArgs>? ConnectionChanged;

        public MqttProtocol(MqttClientFactory mqttClientFactory)
        {
            _mqttClientFactory = mqttClientFactory;
        }

        public async Task Initialize(IProtocolSettings settings)
        {
            if (_mqttClient is not null)
                return;

            _protocolSettings = settings as MqttProtocolSettings;
            var options = BuildConnectionOptions(_protocolSettings);

            _mqttClient = _mqttClientFactory.CreateMqttClient();
            _mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;
            _mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            await _mqttClient.ConnectAsync(options);
        }

        private MqttClientOptions BuildConnectionOptions(MqttProtocolSettings mqttProtocolSettings)
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer(mqttProtocolSettings.Host, mqttProtocolSettings.Port)
                .WithCredentials(mqttProtocolSettings.Username, mqttProtocolSettings.Password)
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .Build();
        }

        public async ValueTask DisposeAsync()
        {
            if (_mqttClient is null)
                return;

            if (_mqttClient.IsConnected)
                await _mqttClient.DisconnectAsync();

            _mqttClient.ConnectedAsync -= MqttClient_ConnectedAsync;
            _mqttClient.DisconnectedAsync -= MqttClient_DisconnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync -= MqttClient_ApplicationMessageReceivedAsync;
            _mqttClient.Dispose();
        }


        #region [ Event Handlers ]
        private async Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(true));

            await _mqttClient.SubscribeAsync(_protocolSettings!.Topic);
        }

        private async Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(false));

            await _mqttClient.UnsubscribeAsync(_protocolSettings!.Topic);
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var applicationMessage = arg.ApplicationMessage;
            var buffer = applicationMessage.Payload.ToArray();

            MessageReceived?.Invoke(this, new MqttMessageReceivedEventArgs(buffer));

            return Task.CompletedTask;
        }
        #endregion
    }
}
