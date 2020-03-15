using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchManager.Model
{
    public class SearchRequest: ISearchRequest
    {
        public string SearchText { get; set; }

        // bool isCaseSensitive { get; set; }
    }
}
