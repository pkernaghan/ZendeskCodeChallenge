namespace ZendeskSearchProcessor.Exception
{
    public class ZendeskSearchProcessorException : System.Exception
    {
        public ZendeskSearchProcessorException()
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