using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchRepository
{
    public interface ISearchRepository
    {
        Task<ISearchResponseData> SearchAll(ISearchRequestData data);
        
        Task<List<Organization>> SearchOrganizations(string searchText);
        Task<List<Ticket>> SearchTickets(string searchText);
        Task<List<User>> SearchUsers(string searchText);
    }
}