using System.Threading.Tasks;
using ZendeskSearchProcessor.Common;
using ZendeskSearchProcessor.Exception;
using ZendeskSearchProcessor.Model;
using ZendeskSearchRepository;
using ZendeskSearchProcessor.Constants;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchProcessor
{
    public class SearchProcessor : ISearchProcessor
    {
        static SearchProcessor()
        {
            SearchRepository = new SearchRepository();
        }

        protected static SearchRepository SearchRepository { get; set; }

        public async Task<ISearchResult> PerformSearch(ISearchRequest searchRequest)
        {
            try
            {
                SearchValidationHelper.ValidateSearchRequest(searchRequest);
                var searchRequestData = CreateRequest(searchRequest);

                var searchResponse = await SearchRepository.SearchAll(searchRequestData);

                ISearchResult result = new SearchResult();
                result.SearchResultDetails = searchResponse;

                return result;
            }
            catch (System.Exception ex)
            {
                throw new ZendeskSearchProcessorException(ErrorMessages.ExecuteSearch.PerformSearch, ex);
            }
        }

        protected static ISearchRequestData CreateRequest(ISearchRequest searchRequest)
        {
            ISearchRequestData requestData = new SearchRequestData();
            requestData.SearchString = searchRequest.SearchText;
            return requestData;
        }
    }
}