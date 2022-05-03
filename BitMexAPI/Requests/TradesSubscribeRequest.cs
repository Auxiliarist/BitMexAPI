using BitMexAPI.Validations;
using System.Runtime.Serialization;

namespace BitMexAPI.Requests
{
    public class TradesSubscribeRequest : SubscribeRequestBase
    {
        /// <summary> Subscribe to all trades </summary>
        public TradesSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary> Subscribe to trades for selected pair ('XBTUSD', etc) </summary>
        public TradesSubscribeRequest(string pair)
        {
            BitmexValidation.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        [IgnoreDataMember]
        public override string Topic => "trade";
        [IgnoreDataMember]
        public override string Symbol { get; }
    }
}