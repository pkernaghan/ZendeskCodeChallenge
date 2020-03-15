using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchProcessor.Model
{
    public class SearchResult: ISearchResult
    {
        public IList<ISearchResultItem> SearchResultItems { get; protected set; }
    }
}
