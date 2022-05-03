using BitMexAPI.Json;
using BitMexAPI.Messages;
using ServiceStack.Text;
using System.Reactive.Subjects;

namespace BitMexAPI.Responses.Books
{
    public class BookResponse : ResponseBase
    {
        public override MessageType Op => MessageType.OrderBook;

        public BookLevel[] Data { get; set; }

        internal static bool TryHandle(JsonObject response, ISubject<BookResponse> subject)
        {
            if (response?["table"]?.ToString() != "orderBookL2")
                return false;

            var parsed = BitmexJsonSerializer.Handle<BookResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}