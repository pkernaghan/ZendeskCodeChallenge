using System;
using System.Collections.Generic;
using System.Text;
using ZendeskSearchRepository.DBModels;

namespace ZendeskSearchRepository.Models
{
    public class SearchResponseData : ISearchResponseData
    {
        public List<Organization> MatchingOrganizations { get; set; }
        public List<User> MatchingUsers { get; set; }
        public List<Ticket> MatchingTickets { get; set; }
    }
}
