using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses.TradeBins
{
    public class TradeBinResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Trade;

        public TradeBin[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<TradeBinResponse> subject)
        {
            if (response?["table"]?.ToString() != "tradeBin1m" &&
                response?["table"]?.ToString() != "tradeBin5m" &&
                response?["table"]?.ToString() != "tradeBin1h" &&
                response?["table"]?.ToString() != "tradeBin1d")
                return false;

            var parsed = BitmexJsonSerializer.Handle<TradeBinResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}