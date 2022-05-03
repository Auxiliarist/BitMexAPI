using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Messages
{
    public enum MessageType
    {
        // Do not rename, used in requests
        Ping,
        authKeyExpires,
        subscribe,
        Unsubscribe,
        CancelAllAfter,

        // Can be renamed, only for responses
        Error,
        Info,
        Trade,
        OrderBook,
        Wallet,
        Order,
        Position,
        Quote
    }
}
