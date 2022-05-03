using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses
{
    public class SubscribeResponse : MessageBase
    {
        public override MessageType Op => MessageType.subscribe;

        public string Subscribe { get; set; }
        public bool Success { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<SubscribeResponse> subject)
        {
            if (response?["subscribe"] != null)
            {
                var parsed = BitmexJsonSerializer.Handle<SubscribeResponse>(response);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}