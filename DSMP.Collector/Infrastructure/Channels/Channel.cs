using DSMP.Collector.Infrastructure.Channels.Events;
using DSMP.Collector.Protocols.Events;
using DSMP.Collector.Protocols.Models;
using DSMP.Collector.Protocols.Mqtt;
using DSMP.Collector.Protocols.Mqtt.Events;

namespace DSMP.Collector.Infrastructure.Channels
{
    internal sealed class Channel : IChannel
    {
        private IMqttProtocol? _mqttProtocol;
        private readonly MqttProtocolSettings _mqttSettings;
        private readonly IMqttProtocolFactory _mqttProtocolFactory;

        public bool Connected { get; private set; }

        public event EventHandler<ChannelConnectionChangedEventArgs>? ConnectionChanged;
        public event EventHandler<ChannelMessageReceivedEventArgs>? MessageReceived;

        public Channel(
            IMqttProtocolFactory mqttProtocolFactory,
            MqttProtocolSettings protocolSettings)
        {
            _mqttSettings = protocolSettings;
            _mqttProtocolFactory = mqttProtocolFactory;
        }

        public async Task Initialize()
        {
            if (_mqttProtocol is not null)
                return;

            _mqttProtocol = _mqttProtocolFactory.Create();
            _mqttProtocol.ConnectionChanged += MqttProtocol_ConnectionChanged;
            _mqttProtocol.MessageReceived += MqttProtocol_MessageReceived;
            await _mqttProtocol.Initialize(_mqttSettings);
        }

        public async ValueTask DisposeAsync()
        {
            if (_mqttProtocol is not null)
            {
                _mqttProtocol.ConnectionChanged -= MqttProtocol_ConnectionChanged;
                _mqttProtocol.MessageReceived -= MqttProtocol_MessageReceived;
                await _mqttProtocol.DisposeAsync();
            }
        }

        private void MqttProtocol_ConnectionChanged(object? sender, ConnectionChangedEventArgs e)
        {
            Connected = e.Connected;
            ConnectionChanged?.Invoke(this,
                new ChannelConnectionChangedEventArgs(e.Connected));
        }

        private void MqttProtocol_MessageReceived(object? sender, MqttMessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, new ChannelMessageReceivedEventArgs(new DeviceData()));
        }
    }
}
