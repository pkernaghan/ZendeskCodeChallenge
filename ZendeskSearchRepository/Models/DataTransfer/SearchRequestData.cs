using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.Models
{
    public class SearchRequestData: ISearchRequestData
    {
        public string SearchString { get; set; }
    }
}
