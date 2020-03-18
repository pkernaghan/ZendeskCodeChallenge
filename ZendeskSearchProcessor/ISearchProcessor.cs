using System.Threading.Tasks;
using ZendeskSearchProcessor.Model;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchProcessor
{
    public interface ISearchProcessor
    {
        Task<ISearchResult> PerformSearch(ISearchRequest searchText);
    }
}