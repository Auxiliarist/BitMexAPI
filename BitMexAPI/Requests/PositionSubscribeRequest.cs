namespace BitMexAPI.Requests
{
    public class PositionSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "position";
    }
}