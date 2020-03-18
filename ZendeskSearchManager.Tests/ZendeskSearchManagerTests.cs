using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FakeItEasy;
using Newtonsoft.Json;
using NUnit.Framework;
using SearchManager;
using ZendeskSearchManager.Constants;
using ZendeskSearchManager.Exception;
using ZendeskSearchProcessor;
using ZendeskSearchProcessor.Model;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;
using ISearchRequest = ZendeskSearchManager.Model.ISearchRequest;
using SearchRequest = ZendeskSearchManager.Model.SearchRequest;

namespace ZendeskSearchManager.Tests
{
    [TestFixture]
    public class ZendeskSearchManagerTests
    {
        [TestCase]
        public void SearchAll_When_SearchRequest_Parameter_IsNull__Then_ExceptionExpected()
        {
            // Arrange
            SearchRequest searchRequest = null;
            ISearchManager testSearchManager = new SearchManager.SearchManager();

            //Act & Assert
            Assert.ThrowsAsync<ZendeskSearchManagerException>(() => testSearchManager.SearchAll(searchRequest));
        }

        #region Validate that Searching with no Search Text value does not cause an exception
        [TestCase]
        public void SearchAll_When_SearchRequest_SearchText_Property_IsNull__Then_NoException()
        {
            // Arrange
            ISearchRequest searchRequest = new SearchRequest();
            ISearchManager testSearchManager = new SearchManager.SearchManager();

            //Act & Assert
            Assert.DoesNotThrowAsync(() => testSearchManager.SearchAll(searchRequest));
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_SearchText_Property_IsEmpty__Then_NoException()
        {
            // Arrange
            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"";
            ISearchManager testSearchManager = new SearchManager.SearchManager();

            //Act & Assert
            Assert.DoesNotThrowAsync(() => testSearchManager.SearchAll(searchRequest));
        }

        [TestCase]
        public void SearchAll_When_SearchRequest_SearchText_Property_IsWhitespaceOnly__Then_NoException()
        {
            // Arrange
            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"        ";
            ISearchManager testSearchManager = new SearchManager.SearchManager();

            //Act & Assert
            Assert.DoesNotThrowAsync(() => testSearchManager.SearchAll(searchRequest));
        }
        #endregion

        [TestCase]
        public async Task DisplayedResponse__When_SearchRequest_SearchText_Is_Empty__Then_DisplayContains_EmptySearchStringAdviceMessage()
        {
            //Arrange
            // Ammend Console Output so it can be verified
            var output = new StringWriter();
            Console.SetOut(output);

            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"      ";
            var testSearchManager = new SearchManager.SearchManager();

            // Act 
            await testSearchManager.SearchAll(searchRequest);

            // Assert
            var consoleText = output.ToString();
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.ResultSummary.EmptySearchStringMessage));
        }

        [TestCase]
        public async Task DisplayedResponse__When_SearchAll__And__SearchResult_Contains_No_Items__Then__DisplayContains_NoMatchingResultMessage()
        {
            //Arrange
            // Ammend Console Output so it can be verified
            var output = new StringWriter();
            Console.SetOut(output);

            ISearchRequest searchRequest = new SearchRequest();
            var testSearchManager = new SearchManager.SearchManager();

            // Arrange Fake SearchProcessor.PerformSearch() to return empty result
            var emptyResult = new SearchResult();
            var fakeSearchProcess = A.Fake<ISearchProcessor>();
            A.CallTo(() => fakeSearchProcess.PerformSearch(A<ISearchRequest>.Ignored)).Returns(emptyResult);
            
            // Act 
            await testSearchManager.SearchAll(searchRequest);

            // Assert
            var consoleText = output.ToString();
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.ResultSummary.NoMatchingResults));
        }


        [TestCase]
        public async Task DisplayedResponse__When_SearchAll__And__SearchResult_Contains_OrganizationItem_Then_DisplayedResultsContain_OrganizationsItemDetails()
        {
            //Arrange
            // Ammend Console Output so it can be verified
            var output = new StringWriter();
            Console.SetOut(output);

            var testOrganisation = DisplayTestHelper.CreateTestOrganization();

            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"Non empty value";

            // Arrange Fake SearchProcessor.PerformSearch() to return empty result
            var searchResult = new SearchResult();
            searchResult.SearchResultDetails = new SearchResponseData();
            searchResult.SearchResultDetails.MatchingOrganizations = new List<Organization>() { testOrganisation };

            // Arrange test SearchManager wherein SearchProcessor is intercepted and substituted in
            var fakeSearchProcessor = A.Fake<ISearchProcessor>();
            A.CallTo(() => fakeSearchProcessor.PerformSearch(A<ISearchRequest>.Ignored)).Returns(searchResult);
            var testSearchManager = new TestableSearchManager();
            testSearchManager.TestSearchProcessor = fakeSearchProcessor;

            // Act 
            await testSearchManager.SearchAll(searchRequest);

            // Assert
            var consoleText = output.ToString();
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.OrganisationDetails.Title));
            Assert.IsTrue(consoleText.Contains(testOrganisation.Id.ToString()));
        }

        [TestCase]
        public async Task DisplayedResponse__When_SearchAll__And__SearchResult_Contains_UserItem_Then_DisplayedResultsContain_UserItemDetails()
        {
            //Arrange
            // Ammend Console Output so it can be verified
            var output = new StringWriter();
            Console.SetOut(output);

            var testComplexUserDto = DisplayTestHelper.CreateTestComplexUserDTO();

            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"Non empty value";

            // Arrange Fake SearchProcessor.PerformSearch() to return empty result
            var searchResult = new SearchResult();
            searchResult.SearchResultDetails = new SearchResponseData();
            searchResult.SearchResultDetails.MatchingUsers = new List<ComplexUserDTO>(){ testComplexUserDto };

            // Arrange test SearchManager wherein SearchProcessor is intercepted and substituted in
            var fakeSearchProcessor = A.Fake<ISearchProcessor>();
            A.CallTo(() => fakeSearchProcessor.PerformSearch(A<ISearchRequest>.Ignored)).Returns(searchResult);
            var testSearchManager = new TestableSearchManager();
            testSearchManager.TestSearchProcessor = fakeSearchProcessor;

            // Act 
            await testSearchManager.SearchAll(searchRequest);

            // Assert
            var consoleText = output.ToString();
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.UserDetails.Title));
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.UserDetails.UserOrganisationSubtitle));
            Assert.IsTrue(consoleText.Contains(testComplexUserDto.UserDTO.Id.ToString()));
            Assert.IsTrue(consoleText.Contains(testComplexUserDto.UserDTO.OrganizationId.ToString()));
            Assert.IsTrue(consoleText.Contains(testComplexUserDto.OrganizationDTO.Id.ToString()));
            Assert.IsTrue(consoleText.Contains(testComplexUserDto.OrganizationDTO.ExternalId.ToString()));
        }

        [TestCase]
        public async Task DisplayedResponse__When_SearchAll__And__SearchResult_Contains_ComplexTicketItem_Then_DisplayedResultsContain_ComplexTicketItemDetails()
        {
            //Arrange
            // Ammend Console Output so it can be verified
            var output = new StringWriter();
            Console.SetOut(output);

            var testComplexTicketDto = DisplayTestHelper.CreateTestComplexTicketDTO();

            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"Non empty value";

            // Arrange Fake SearchProcessor.PerformSearch() to return empty result
            var searchResult = new SearchResult();
            searchResult.SearchResultDetails = new SearchResponseData();
            searchResult.SearchResultDetails.MatchingTickets = new List<ComplexTicketDTO>() { testComplexTicketDto };

            // Arrange test SearchManager wherein SearchProcessor is intercepted and substituted in
            var fakeSearchProcessor = A.Fake<ISearchProcessor>();
            A.CallTo(() => fakeSearchProcessor.PerformSearch(A<ISearchRequest>.Ignored)).Returns(searchResult);
            var testSearchManager = new TestableSearchManager();
            testSearchManager.TestSearchProcessor = fakeSearchProcessor;

            // Act 
            await testSearchManager.SearchAll(searchRequest);

            // Assert
            var consoleText = output.ToString();
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.TicketDetails.Title));
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.TicketDetails.TicketAssigneeSubtitle));
            Assert.IsTrue(consoleText.Contains(PrintSearchResultMessages.TicketDetails.TicketSubmitterSubtitle));
            Assert.IsTrue(consoleText.Contains(testComplexTicketDto.Ticket.Id.ToString()));
            Assert.IsTrue(consoleText.Contains(testComplexTicketDto.Ticket.AssigneeId.ToString()));
        }
    }
}
