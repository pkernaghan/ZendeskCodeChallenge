using System.Collections.Generic;
using ZendeskSearchRepository.DBModels;

namespace ZendeskSearchRepository.Models
{
    public interface ISearchResponseData
    {
        List<Organization> MatchingOrganizations { get; set; }
        List<User> MatchingUsers { get; set; }
        List<Ticket> MatchingTickets { get; set; }
    }
}