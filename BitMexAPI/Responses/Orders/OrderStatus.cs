using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Responses.Orders
{
    public enum OrderStatus
    {
        Undefined,
        New,
        Filled,
        PartiallyFilled,
        Canceled
    }
}
