using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace BitMexAPI.Responses.Orders
{
    public class OrderResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Order;

        public Order[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<OrderResponse> subject)
        {
            if (response?["table"]?.ToString() != "order")
                return false;

            var parsed = BitmexJsonSerializer.Handle<OrderResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
