using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace BitMexAPI.Responses
{
    public class AuthenticationResponse : MessageBase
    {
        public override MessageType Op => MessageType.authKeyExpires;

        public bool Success { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<AuthenticationResponse> subject)
        {
            if (response?.Object("request")?["op"]?.ToString() == "authKeyExpires")
            {
                var parsed = BitmexJsonSerializer.Handle<AuthenticationResponse>(response);
                subject.OnNext(parsed);
                return true;
            }

            return false;
        }
    }
}
