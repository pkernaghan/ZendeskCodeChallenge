using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchProcessor.Exception
{
    public class ZendeskSearchProcessorException: System.Exception
    {
        public ZendeskSearchProcessorException() : base()
        {
        }

        public ZendeskSearchProcessorException(string message) : base(message)
        {
        }

        public ZendeskSearchProcessorException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
