namespace BitMexAPI.Requests
{
    public class LiquidationSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "liquidation";
    }
}