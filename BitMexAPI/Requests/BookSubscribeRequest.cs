using BitMexAPI.Validations;
using System.Runtime.Serialization;

namespace BitMexAPI.Requests
{
    public class BookSubscribeRequest : SubscribeRequestBase
    {
        /// <summary> Subscribe to order book from all pairs </summary>
        public BookSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary> Subscribe to order book from selected pair ('XBTUSD', etc) </summary>
        public BookSubscribeRequest(string pair)
        {
            BitmexValidation.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        [IgnoreDataMember]
        public override string Topic => "orderBookL2";
        [IgnoreDataMember]
        public override string Symbol { get; }
    }
}