namespace ZendeskSearchManager.Constants
{
    public static class PrintSearchResultMessages
    {
        public static class ResultSummary
        {
            public static string EmptySearchStringMessage = @"Search String Is empty. Please search again.";
            public static string NoMatchingResults = @"No Matching Results found.";
        }

        public static class OrganisationDetails
        {
            public static string Title = @"Organisation Details: ";
        }

        public static class UserDetails
        {
            public static string Title = @"User Details: ";
            public static string UserOrganisationSubtitle = @"User's Organisation Details: ";
        }

        public static class TicketDetails
        {
            public static string Title = @"Ticket Details: ";
            public static string TicketOrganisationSubtitle = @"Ticket's Organisation Details: ";
            public static string TicketSubmitterSubtitle = @"Ticket's Submitter Details: ";
            public static string TicketAssigneeSubtitle = @"Ticket's Assignee Details: ";
        }
    }
}