using System.Collections.Generic;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;

namespace ZendeskSearchRepository.Models
{
    public interface ISearchResponseData
    {
        List<Organization> MatchingOrganizations { get; set; }
        List<ComplexUserDTO> MatchingUsers { get; set; }
        List<ComplexTicketDTO> MatchingTickets { get; set; }
    }
}