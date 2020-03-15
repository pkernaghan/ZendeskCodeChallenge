using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchManager.Exception
{
    public class ZendeskSearchManagerException: System.Exception
    {
        public ZendeskSearchManagerException() : base()
        {
        }

        public ZendeskSearchManagerException(string message) : base(message)
        {
        }

        public ZendeskSearchManagerException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
