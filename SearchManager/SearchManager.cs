using System;
using System.Collections.Generic;
using ZendeskSearchManager.Exception;
using ZendeskSearchManager.Model;

namespace SearchManager
{
    public class SearchManager
    {
        public ISearchResponse SearchAll(ISearchRequest searchRequest)
        {
            try
            {
                ValidateSearchRequest(searchRequest);
                return ProcessSearchRequest(searchRequest);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException();
            }
        }

        protected ISearchResponse ProcessSearchRequest(ISearchRequest searchRequest)
        {
            ISearchResponse response = new SearchResponse();

            try
            {
                ValidateSearchRequest(searchRequest);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException();
            }

            return response;
        }

        protected void ValidateSearchRequest(ISearchRequest searchRequest)
        {

        }
    }
}
