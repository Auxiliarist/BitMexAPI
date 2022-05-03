namespace BitMexAPI.Requests
{
    public class WalletSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "wallet";
    }
}