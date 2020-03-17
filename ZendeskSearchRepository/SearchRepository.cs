using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskSearchManager.Exception;
using ZendeskSearchRepository.Configuration;
using ZendeskSearchRepository.Constants;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.Exceptions;
using ZendeskSearchRepository.Models;

namespace ZendeskSearchRepository
{
    public class SearchRepository : ISearchRepository
    {
        #region Static Properties

        #region Public static Properties
        //public static List<Organization> GetOrganizations() { return _organizations; }
        //public static List<User> GetUsers() { return _users; }
        //public static List<Ticket> GetTickets() { return _tickets; }
        #endregion

        #region Protected static Properties

        protected static List<Organization> _organizations = new List<Organization>();        
        protected static List<User> _users = new List<User>();
        protected static List<Ticket> _tickets = new List<Ticket>();
        protected static ISearchRepoConfigSettings configSettings;
        #endregion
        
        #endregion
        static SearchRepository()
        {
            GetConfigSettings();
            PopulateDateRepo(configSettings);
        }

        public SearchRepository()
        {
        }

        #region Public methods
        public async Task<ISearchResponseData> SearchAll(ISearchRequestData data)
        {
            var response = new SearchResponseData();

            response.MatchingOrganizations = await SearchOrganizations(data.SearchString);
            response.MatchingTickets = await SearchTickets(data.SearchString);
            response.MatchingUsers = await SearchUsers(data.SearchString);

            return response;
        }

        public async Task<List<Organization>> SearchOrganizations(string searchText)
        {
            var returnList = new List<Organization>();

            try
            {

                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                {
                    return returnList;
                }
                else
                {
                    return GetMatchingObjectsContaningMatchingString<Organization>(searchText, _organizations);
                }
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException(ErrorMessages.SearchData.SearchOrganizations);
            }
        }

        public async Task<List<Ticket>> SearchTickets(string searchText)
        {
            var returnList = new List<Ticket>();

            try
            {

                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                {
                    return returnList;
                }
                else
                {
                    return GetMatchingObjectsContaningMatchingString<Ticket>(searchText, _tickets);
                }
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException(ErrorMessages.SearchData.SearchTickets);
            }
        }

        public async Task<List<User>> SearchUsers(string searchText)
        {
            var returnList = new List<User>();

            try
            {

                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                {
                    return returnList;
                }
                else
                {
                    return GetMatchingObjectsContaningMatchingString<User>(searchText, _users);
                }
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchManagerException(ErrorMessages.SearchData.SearchUser);
            }
        }

        protected List<Organization> GetMatchingObjectsContaningMatchingString(string searchText)
        {
            var matchingOrganisations = new List<Organization>();

            var allOrganization = _organizations;

            foreach (var org in allOrganization)
            {
                if (!String.IsNullOrWhiteSpace(org.JsonData) && org.JsonData.ToLower().Contains(searchText.ToLower()))
                {
                    matchingOrganisations.Add(org);
                }
            }
            return matchingOrganisations;
        }

        protected List<T> GetMatchingObjectsContaningMatchingString<T>(string searchText, List<T> entityList) where T : RepoItem, new()
        {
            var matchingOrganisations = new List<T>();

            foreach (var item in entityList)
            {
                if (!String.IsNullOrWhiteSpace(item.JsonData) && item.JsonData.ToLower().Contains(searchText.ToLower()))
                {
                    matchingOrganisations.Add(item);
                }
            }
            return matchingOrganisations;
        }

        //protected Task<List<Ticket>> SearchTickets(string searchText)
        //{

        //}

        //protected Task<List<User>> SearchUsers(string searchText)
        //{

        //}

        #endregion

        #region Static Memebers
        protected static void PopulateDateRepo(ISearchRepoConfigSettings configSettings)
        {
            try
            {
                LoadOrganizations(configSettings.OrganizationsFilePath);
                LoadUsers(configSettings.UsesFilePath);
                LoadTickets(configSettings.TicketsFilePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.GenericMessage, ex);
            }
        }

        protected static void LoadOrganizations(string filePath)
        {
            try { 
                LoadRepoItems<Organization>(out _organizations, filePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.OrganizationDataMessage , ex);
            }
        }

        protected static void LoadUsers(string filePath)
        {
            try
            {
                LoadRepoItems<User>(out _users, filePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.UserDataMessage, ex);
            }
        }

        protected static void LoadTickets(string filePath)
        {
            try
            {
                LoadRepoItems<Ticket>(out _tickets, filePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.TicketsDataMessage, ex);
            }
        }

        protected static void LoadRepoItems<T>(out List<T> listToBePopulated, string jsonFilePath) where T : RepoItem, new()
        {
            listToBePopulated = new List<T>();

            List <RepoItem> returnList = new List<RepoItem>();

            using (StreamReader file = File.OpenText(jsonFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var aRepoItems = JToken.ReadFrom(reader);
                JEnumerable<JToken> repoItemList = aRepoItems.Children();

                foreach (JToken jsonRepoItem in repoItemList)
                {
                    dynamic repoItem;
                    var id = jsonRepoItem["_id"];
                    var externalId = jsonRepoItem["external_id"];
                    var jsonData = jsonRepoItem.ToString();

                    if (typeof(T) == typeof(Organization))
                    {
                        repoItem = new Organization((int?)id, (Guid?) externalId, jsonData);
                        listToBePopulated.Add(repoItem);

                    } else if (typeof(T) == typeof(User))
                    {
                        var organizationId = jsonRepoItem["organization_id"];
                        repoItem = new User((int?)organizationId, (int?)id, (Guid?)externalId, jsonData);
                        listToBePopulated.Add(repoItem);

                    } else if (typeof(T) == typeof(Ticket))
                    {
                        var organizationId = jsonRepoItem["organization_id"];
                        var submitterId = jsonRepoItem["submitter_id"];
                        var assigneeId = jsonRepoItem["assignee_id"];

                        repoItem = new Ticket()
                        {
                            Id = (Guid?)id,
                            ExternalId = (Guid?)externalId,
                            OrganizationId = (int?)organizationId,
                            SubmitterId = (int?)submitterId,
                            AssigneeId =  (int?)assigneeId,
                            JsonData = jsonData
                        };

                        listToBePopulated.Add(repoItem);
                    }
                }
            }
        }

        protected static void GetConfigSettings()
        {
            // Dependency Injection Setup, Config File values loaded from JSON file where they're defined, and used to initialise PrintProcessing Manager
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            configSettings = new SearchRepoConfigSettings(config);
        }
        #endregion
    }
}
