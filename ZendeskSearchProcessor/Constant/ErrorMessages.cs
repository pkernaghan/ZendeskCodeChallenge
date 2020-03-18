namespace ZendeskSearchProcessor.Constants
{
    public class ErrorMessages
    {
        public static class SearchRequestValidation
        {
            public static string SearchRequestNull = @"Error: SearchRequestObject Cannot be null.";

            public static string SearchTextEmpty =
                @"Error: Search Text cannot be null, whitespace, or empty. Please enter search Text";
        }

        public static class ExecuteSearch
        {
            public static string PerformSearch =
                @"Error: A exception occurred whilst performing a search. Please see inner exception for more infomration";
        }
    }
}