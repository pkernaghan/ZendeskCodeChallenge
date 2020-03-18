using System;
using System.Collections.Generic;
using System.Text;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;

namespace ZendeskSearchRepository.Models
{
    public class SearchResponseData : ISearchResponseData
    {
        public List<Organization> MatchingOrganizations { get; set; }
        public List<ComplexUserDTO> MatchingUsers { get; set; }
        public List<ComplexTicketDTO> MatchingTickets { get; set; }

        public SearchResponseData()
        {
            MatchingOrganizations = new List<Organization>();
            MatchingUsers = new List<ComplexUserDTO>();
            MatchingTickets = new List<ComplexTicketDTO>();
        }
    }
}
