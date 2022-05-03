using BitMexAPI.Messages;
using System;
using System.Runtime.Serialization;

namespace BitMexAPI.Requests
{
    public abstract class SubscribeRequestBase : RequestBase
    {
        [IgnoreDataMember]
        public override MessageType Operation => MessageType.subscribe;

        public string[] Args => new[]
        {
            string.IsNullOrWhiteSpace(Symbol) ? Topic : $"{Topic}:{Symbol}"
        };

        [IgnoreDataMember]
        public abstract string Topic { get; }

        [IgnoreDataMember]
        public virtual string Symbol { get; } = string.Empty;
    }
}