using System;
using NUnit.Framework;

namespace ZendeskSearchManager.Tests
{
    [TestFixture]
    public class ZendeskSearchManagerTests
    {
        [TestCase]
        public void SearchAll_When_SearchRequest_Parameter_IsInvalid__Then_ExceptionExpected()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_Parameter_Contain_Text_Matching_No_Items_Then_ResponseContainsNoMatchingResultItems()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_Contains_Text_Matching_Single_Item_Then_ResponseContainsSingleItem()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_Contains_Text_Matching_Single_Item_Then_ResponseContainsAssociatedDataForSingleItem()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_Contains_Text_Matching_Multiple_Items_Then_ResponseContainsAllMultipleItems()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_Contains_Text_Matching_Multiple_Items_Then_ResponseContainsAssociatedDataForAllMultipleItems()
        {
            throw new NotImplementedException();
        }
    }
}
