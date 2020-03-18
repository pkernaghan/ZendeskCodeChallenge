using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchManager.Exception
{
    public class PrintDisplayException : System.Exception
    {
        public PrintDisplayException() : base()
        {
        }

        public PrintDisplayException(string message) : base(message)
        {
        }

        public PrintDisplayException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
