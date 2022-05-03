using BitMexAPI.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BitMexAPI.Requests
{
    public abstract class RequestBase : MessageBase
    {
        public override MessageType Op
        {
            get => Operation;
            set { }
        }

        [IgnoreDataMember]
        public abstract MessageType Operation { get; }

        [IgnoreDataMember]
        public virtual string OperationString => Operation.ToString().ToLower();

        [IgnoreDataMember]
        public virtual bool IsRaw { get; } = false;
    }
}
