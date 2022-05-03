using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses.Trades
{
    public class TradeResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Trade;

        public Trade[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<TradeResponse> subject)
        {
            if (response?["table"]?.ToString() != "trade")
                return false;
         
            var parsed = BitmexJsonSerializer.Handle<TradeResponse>(response);
            subject.OnNext(parsed);            
            return true;
        }
    }
}