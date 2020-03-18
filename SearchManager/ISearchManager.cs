using System.Threading.Tasks;
using ZendeskSearchProcessor.Model;

namespace ZendeskSearchManager
{
    public interface ISearchManager
    {
        Task SearchAll(ISearchRequest searchRequest);
    }
}