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

        //Task<ISearchResponseData> SearchOrgnisations(ISearchRequestData data);
        //Task<ISearchResponseData> SearchTickets(ISearchRequestData data);
        //Task<ISearchResponseData> SearchUsers(ISearchRequestData data);
    }
}