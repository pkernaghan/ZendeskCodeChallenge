using System;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchRepository
{
    public class SearchRepository : ISearchRepository
    {
        public ISearchResponseData GetData(ISearchRequestData searchRequestData)
        {
            var searchResponseData = new SearchResponseData();

            return searchResponseData;
        }

    }
}
