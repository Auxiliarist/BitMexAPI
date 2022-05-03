using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace BitMexAPI.Responses.Quotes
{
    public class QuoteResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Quote;

        public Quote[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<QuoteResponse> subject)
        {
            if (response?["table"]?.ToString() != "quote")
                return false;

            var parsed = BitmexJsonSerializer.Handle<QuoteResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
