using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.Exceptions
{
    public class ZendeskSearchRepositoryException: Exception
    {
        public ZendeskSearchRepositoryException() : base()
        {
        }

        public ZendeskSearchRepositoryException(string message) : base(message)
        {
        }

        public ZendeskSearchRepositoryException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
