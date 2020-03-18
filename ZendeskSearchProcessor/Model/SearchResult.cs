using ZendeskSearchRepository.Models;

namespace ZendeskSearchProcessor.Model
{
    public class SearchResult : ISearchResult
    {
        public ISearchResponseData SearchResultDetails { get; set; }
    }
}