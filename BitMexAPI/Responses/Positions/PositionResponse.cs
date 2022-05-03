using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses.Positions
{
    public class PositionResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Position;

        public Position[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<PositionResponse> subject)
        {
            if (response?["table"]?.ToString() != "position")
                return false;

            var parsed = BitmexJsonSerializer.Handle<PositionResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}