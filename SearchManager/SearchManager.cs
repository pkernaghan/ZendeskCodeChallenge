using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskSearchManager.Exception;
using ZendeskSearchManager.Model;
using ZendeskSearchProcessor;
using ZendeskSearchProcessor.Model;

namespace SearchManager
{
    public class SearchManager
    {
        protected ISearchProcessor searchProcessor;

        public SearchManager()
        {
            searchProcessor = new SearchProcessor();
        }

        public async Task<ISearchResponse> SearchAll(ISearchRequest searchRequest)
        {
            try
            {
                ValidateSearchRequest(searchRequest);
                return await ProcessSearchRequest(searchRequest);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException();
            }
        }

        protected async Task<ISearchResponse> ProcessSearchRequest(ISearchRequest searchRequest)
        {
            ISearchResponse response = new SearchResponse();

            try
            {
                var requestPayload = PrepareSearchRequestPayload(searchRequest);

                ISearchResult result = await searchProcessor.SearchAll(requestPayload);

                return ParseSearchResponseDetails(result);

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

        protected ISearchRequestItem PrepareSearchRequestPayload(ISearchRequest searchRequest)
        {
            var requestPayload = new SearchRequestItem();

            return requestPayload;
        }

        protected ISearchResponse ParseSearchResponseDetails(ISearchResult searchResults)
        {
            var requestResponse = new SearchResponse();

            return requestResponse;
        }
    }
}
