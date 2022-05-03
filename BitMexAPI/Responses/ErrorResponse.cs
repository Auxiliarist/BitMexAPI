using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses
{
    public class ErrorResponse : MessageBase
    {
        public override MessageType Op => MessageType.Error;

        public double? Status { get; set; }
        public string Error { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        public Dictionary<string, object> Request { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<ErrorResponse> subject)
        {
            if (response?["error"] != null)
            {
                var parsed = BitmexJsonSerializer.Handle<ErrorResponse>(response);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}