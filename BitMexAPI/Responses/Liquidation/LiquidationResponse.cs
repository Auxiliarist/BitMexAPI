using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses.Liquidation
{
    public class LiquidationResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Position;

        public Liquidation[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<LiquidationResponse> subject)
        {
            if (response?["table"]?.ToString() != "liquidation")
                return false;

            var parsed = BitmexJsonSerializer.Handle<LiquidationResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}