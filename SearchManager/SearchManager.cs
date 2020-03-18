using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskSearchManager;
using ZendeskSearchManager.Common;
using ZendeskSearchManager.Exception;
using ZendeskSearchProcessor;
using ZendeskSearchProcessor.Common;
using ZendeskSearchProcessor.Model;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;
using ZendeskSearchManager.Constants;

namespace SearchManager
{
    public class SearchManager : ISearchManager
    {
        #region Public Methods

        public async Task SearchAll(ISearchRequest searchRequest)
        {
            try
            {
                SearchValidationHelper.ValidateSearchRequest(searchRequest);

                if (String.IsNullOrWhiteSpace(searchRequest.SearchText))
                {
                    Console.WriteLine(PrintSearchResultMessages.ResultSummary.EmptySearchStringMessage);
                }

                await ExecuteSearch(searchRequest, new SearchProcessor());
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException(ErrorMessages.ExecuteSearch.PerformSearch, ex);
            }
        }

        protected virtual async Task ExecuteSearch(ISearchRequest searchRequest, ISearchProcessor searchProcessor)
        {
            var searchResult = await searchProcessor.PerformSearch(searchRequest);
                DisplaySearchResults(searchResult);
        }

        #endregion

        #region Static Methods

        #region Results Evaluation Logic

        public static bool IsResultsFound(ISearchResult searchResponseData)
        {
            if (searchResponseData?.SearchResultDetails == null) return false;

            var searchResultDetails = searchResponseData.SearchResultDetails;

            return IsMatchingOrganisationResultsFound(searchResultDetails.MatchingOrganizations)
                   || IsMatchingTicketResultsFound(searchResultDetails.MatchingTickets)
                   || IsMatchingUserResultsFound(searchResultDetails.MatchingUsers);
        }

        public static bool IsMatchingOrganisationResultsFound(List<Organization> organizationList)
        {
            return organizationList != null && organizationList.Count > 0;
        }

        public static bool IsMatchingUserResultsFound(List<ComplexUserDTO> userList)
        {
            return userList != null && userList.Count > 0;
        }

        public static bool IsMatchingTicketResultsFound(List<ComplexTicketDTO> ticketList)
        {
            return ticketList != null && ticketList.Count > 0;
        }

        #endregion

        #region Results Evaluation Logic

        public static void DisplaySearchResults(ISearchResult searchResponseData)
        {
            if (!IsResultsFound(searchResponseData))
            {
                Console.WriteLine(PrintSearchResultMessages.ResultSummary.NoMatchingResults);
            }
            else
            {
                var searchResultDetails = searchResponseData.SearchResultDetails;

                DisplayOrganizationSearchResults(searchResultDetails.MatchingOrganizations);
                DisplayTicketSearchResults(searchResultDetails.MatchingTickets);
                DisplayUserSearchResults(searchResultDetails.MatchingUsers);
            }
        }

        public static void DisplayOrganizationSearchResults(List<Organization> organizationList)
        {
            if (IsMatchingOrganisationResultsFound(organizationList))
                foreach (var org in organizationList)
                    PrintDisplayHelper.PrintOrganisationDetails(org);
        }

        public static void DisplayTicketSearchResults(List<ComplexTicketDTO> ticketList)
        {
            if (IsMatchingTicketResultsFound(ticketList))
                foreach (var ticket in ticketList)
                    PrintDisplayHelper.PrintTicketDetails(ticket);
        }

        public static void DisplayUserSearchResults(List<ComplexUserDTO> userList)
        {
            if (IsMatchingUserResultsFound(userList))
                foreach (var user in userList)
                    PrintDisplayHelper.PrintUserDetails(user);
        }

        #endregion

        #endregion
    }
}