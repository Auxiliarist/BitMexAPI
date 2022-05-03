using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses
{
    public class InfoResponse : MessageBase
    {
        public override MessageType Op => MessageType.Info;

        public string Info { get; set; }
        public DateTime Version { get; set; }
        public DateTime Timestamp { get; set; }
        public string Docs { get; set; }
        public Dictionary<string, object> Limit { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<InfoResponse> subject)
        {
            if (response?["info"] != null && response?["version"] != null)
            {
                var parsed = BitmexJsonSerializer.Handle<InfoResponse>(response);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}