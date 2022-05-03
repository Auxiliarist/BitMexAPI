using BitMexAPI.Validations;

namespace BitMexAPI.Requests
{
    public class SettlementSubscribeRequest : SubscribeRequestBase
    {
        /// <summary> Subscribe to all settlements </summary>
        public SettlementSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary> Subscribe to settlements for selected pair ('XBTUSD', etc) </summary>
        public SettlementSubscribeRequest(string pair)
        {
            BitmexValidation.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        public override string Topic => "settlement";
        public override string Symbol { get; }
    }
}