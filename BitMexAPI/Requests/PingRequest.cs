using BitMexAPI.Messages;

namespace BitMexAPI.Requests
{
    public class PingRequest : RequestBase
    {
        public override MessageType Operation => MessageType.Ping;

        public override bool IsRaw => true;
    }
}