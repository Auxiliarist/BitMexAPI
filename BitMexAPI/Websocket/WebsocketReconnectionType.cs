using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Websocket
{
    public enum WebsocketReconnectionType
    {
        /// <summary>
        /// Type used for initial connection to websocket stream
        /// </summary>
        Initial,

        /// <summary>
        /// Type used when connection to websocket was lost in meantime
        /// </summary>
        Lost,

        /// <summary>
        /// Type used when connection to websocket was lost by not receiving any message in given timerange
        /// </summary>
        NoMessageReceived,

        /// <summary>
        /// Type used after unsuccessful previous reconnection
        /// </summary>
        Error
    }
}
