using System.Threading.Tasks;
using ZendeskSearchProcessor.Model;

namespace ZendeskSearchProcessor
{
    public interface ISearchProcessor
    {
        Task<ISearchResult> SearchAll(ISearchRequestItem searchRequestItem);
    }
}