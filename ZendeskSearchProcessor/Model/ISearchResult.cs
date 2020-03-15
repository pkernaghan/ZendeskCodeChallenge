using System.Collections.Generic;

namespace ZendeskSearchProcessor.Model
{
    public interface ISearchResult
    {
        IList<ISearchResultItem> SearchResultItems { get;}
    }
}