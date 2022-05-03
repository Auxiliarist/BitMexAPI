using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Exceptions
{
    public class BitmexException : Exception
    {
        public BitmexException()
        {
        }

        public BitmexException(string message)
            : base(message)
        {
        }

        public BitmexException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
