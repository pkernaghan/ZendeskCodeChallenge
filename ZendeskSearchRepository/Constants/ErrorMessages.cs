using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.Constants
{
    public class ErrorMessages
    {
        public static class LoadData
        {
            public static string GenericMessage = @"Error: An error Occurred when loading Repo Data";
            public static string UserDataMessage = @"Error: An error Occurred when loading Users Repo Data";
            public static string TicketsDataMessage = @"Error: An error Occurred when loading Tickets Repo Data";

            public static string OrganizationDataMessage =
                @"Error: An error Occurred when loading Organizations Repo Data";
        }

        public static class SearchData
        {
            public static string SearchOrganizations = @"Error: An error Occurred when Search Organizations Repo Data";
            public static string SearchTickets = @"Error: An error Occurred when Search Tickets Repo Data";
            public static string SearchUser = @"Error: An error Occurred when Search Users Repo Data";
        }
    }
}
