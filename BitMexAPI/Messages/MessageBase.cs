using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Messages
{
    public abstract class MessageBase
    {
        public virtual MessageType Op { get; set; }
    }
}
