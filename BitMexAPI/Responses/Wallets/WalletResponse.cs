using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace BitMexAPI.Responses.Wallets
{
    public class WalletResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Wallet;

        public Wallet[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<WalletResponse> subject)
        {
            if (response?["table"]?.ToString() != "wallet")
                return false;
            
            var parsed = BitmexJsonSerializer.Handle<WalletResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }

}
