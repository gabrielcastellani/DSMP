namespace DSMP.Collector.Protocols.Mqtt.Events
{
    public sealed class MqttMessageReceivedEventArgs : EventArgs
    {
        public byte[] Buffer { get; private set; }

        public MqttMessageReceivedEventArgs(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
}
