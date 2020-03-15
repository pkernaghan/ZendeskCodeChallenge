using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskSearchProcessor.Model;

namespace ZendeskSearchProcessor
{
    public class SearchProcessor : ISearchProcessor
    {

        public async Task<ISearchResult> SearchAll(ISearchRequestItem searchRequestItem)
        {
            ISearchResult searchResult = new SearchResult();

            // IList<ISearchResultItem> results = new List<SearchResultItem>();


            return searchResult;
        }
    }
}
