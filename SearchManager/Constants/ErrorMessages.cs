using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchManager.Constants
{
    public class ErrorMessages
    {
        public static class ExecuteSearch
        {
            public static string PerformSearch =
                @"Error: A exception occurred whilst performing a search. Please see inner exception for more information";
        }

        public static class PrintDisplay
        {
            public static string DisplaySearchResults = 
                @"Error: A exception occurred whilst displaying Search Results";
            public static string DisplayOrganisationDetails = 
                @"Error: A exception occurred whilst displaying Organisation Details";
            public static string DisplayTicketDetails = 
                @"Error: A exception occurred whilst displaying Ticket Details";            
            public static string DisplayUserDetails = 
                @"Error: A exception occurred whilst displaying User Details";
        }

    }
}
