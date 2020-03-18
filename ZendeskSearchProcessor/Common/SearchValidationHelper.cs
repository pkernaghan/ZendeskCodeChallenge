using System;
using ZendeskSearchProcessor.Model;
using ZendeskSearchProcessor.Constants;

namespace ZendeskSearchProcessor.Common
{
    public static class SearchValidationHelper
    {
        public static void ValidateSearchRequest(ISearchRequest searchRequest)
        {
            if (searchRequest == null)
                throw new ArgumentNullException(ErrorMessages.SearchRequestValidation.SearchRequestNull);

            ValidateSearchText(searchRequest.SearchText);
        }

        public static void ValidateSearchText(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException(ErrorMessages.SearchRequestValidation.SearchTextEmpty);
        }
    }
}