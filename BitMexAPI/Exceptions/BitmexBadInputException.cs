using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Exceptions
{
    public class BitmexBadInputException : BitmexException
    {
        public BitmexBadInputException()
        {
        }

        public BitmexBadInputException(string message) : base(message)
        {
        }

        public BitmexBadInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
