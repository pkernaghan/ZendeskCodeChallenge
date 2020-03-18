using System.Threading.Tasks;
using ZendeskSearchProcessor;
using ZendeskSearchProcessor.Model;

namespace ZendeskSearchManager.Tests
{
    public class TestableSearchManager : SearchManager.SearchManager
    {
        public TestableSearchManager(): base()
        {
        }

        public SearchResult TestSearchResult { get; set; }
        public ISearchProcessor TestSearchProcessor { get; set; }

        protected override async Task ExecuteSearch(ZendeskSearchProcessor.Model.ISearchRequest searchRequest, ISearchProcessor searchProcessor)
        {
            var searchResult = await TestSearchProcessor.PerformSearch(searchRequest);
            DisplaySearchResults(searchResult);
        }

    }
}