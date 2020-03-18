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
        }
    }
}