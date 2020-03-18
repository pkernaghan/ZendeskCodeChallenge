using System.Collections.Generic;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchProcessor.Model
{
    public interface ISearchResult
    {
        ISearchResponseData SearchResultDetails { get; set; }
    }
}