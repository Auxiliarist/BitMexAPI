namespace BitMexAPI.Requests
{
    public class OrderSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "order";
    }
}