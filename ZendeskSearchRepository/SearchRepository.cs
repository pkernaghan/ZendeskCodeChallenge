using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskSearchRepository.Configuration;
using ZendeskSearchRepository.Constants;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Exceptions;
using ZendeskSearchRepository.Models;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;

namespace ZendeskSearchRepository
{
    public class SearchRepository : ISearchRepository
    {
        static SearchRepository()
        {
            GetConfigSettings();
            PopulateDateRepo(configSettings);
        }

        #region Static Properties

        #region Protected static Properties

        protected static List<Organization> _organizations = new List<Organization>();
        protected static List<User> _users = new List<User>();
        protected static List<Ticket> _tickets = new List<Ticket>();
        protected static ISearchRepoConfigSettings configSettings;

        #endregion

        #endregion

        #region Public methods

        public async Task<ISearchResponseData> SearchAll(ISearchRequestData data)
        {
            try
            {
                var response = new SearchResponseData();

                response.MatchingOrganizations = await SearchOrganizations(data.SearchString);
                var lazyTicketsList = await SearchTickets(data.SearchString);
                var lazyUsersList = await SearchUsers(data.SearchString);

                foreach (var ticket in lazyTicketsList)
                {
                    var complexDTO = GetComplexTicketById(ticket.Id);

                    try
                    {
                        response.MatchingTickets.Add(complexDTO);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException();
                    }
                }

                foreach (var user in lazyUsersList)
                {
                    var complexUserDTO = GetComplexUserById(user.Id);
                    response.MatchingUsers.Add(complexUserDTO);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.SearchData.GenericMessage);
            }
        }

        public async Task<List<Organization>> SearchOrganizations(string searchText)
        {
            var returnList = new List<Organization>();

            try
            {
                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                    return returnList;
                return GetMatchingObjectsContaningMatchingString(searchText, _organizations);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.SearchData.SearchOrganizations);
            }
        }

        public async Task<List<Ticket>> SearchTickets(string searchText)
        {
            var returnList = new List<Ticket>();

            try
            {
                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                    return returnList;
                return GetMatchingObjectsContaningMatchingString(searchText, _tickets);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.SearchData.SearchTickets);
            }
        }

        public async Task<List<User>> SearchUsers(string searchText)
        {
            var returnList = new List<User>();

            try
            {
                if (string.IsNullOrWhiteSpace(searchText) || _organizations == null || _organizations.Count == 0)
                    return returnList;
                return GetMatchingObjectsContaningMatchingString(searchText, _users);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.SearchData.SearchUser);
            }
        }

        protected List<T> GetMatchingObjectsContaningMatchingString<T>(string searchText, List<T> entityList)
            where T : RepoItem, new()
        {
            var matchingOrganisations = new List<T>();

            foreach (var item in entityList)
            {
                try
                {
                    var jsontemp = item.JsonData;
                    var jsontemp2 = jsontemp.Replace("\"", "");
                    var jsonData = Regex.Replace(jsontemp2, @"\t|\n|\r", "");

                    if (!string.IsNullOrWhiteSpace(jsonData) &&
                        jsonData.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))

                        matchingOrganisations.Add(item);
                }                     
                catch (Exception ex)
                {
                    Console.WriteLine(@"Error adding item to generic list of Type:" + typeof(T) + ". Continuing on with rest...");
                }
            }

            return matchingOrganisations;
        }

        #endregion

        #region LoadData

        protected static void GetConfigSettings()
        {
            // Dependency Injection Setup, Config File values loaded from JSON file where they're defined, and used to initialise PrintProcessing Manager
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            configSettings = new SearchRepoConfigSettings(config);
        }

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
            try
            {
                LoadRepoItems(out _organizations, filePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.OrganizationDataMessage, ex);
            }
        }

        protected static void LoadUsers(string filePath)
        {
            try
            {
                LoadRepoItems(out _users, filePath);
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
                LoadRepoItems(out _tickets, filePath);
            }
            catch (Exception ex)
            {
                throw new ZendeskSearchRepositoryException(ErrorMessages.LoadData.TicketsDataMessage, ex);
            }
        }

        protected static void LoadRepoItems<T>(out List<T> listToBePopulated, string jsonFilePath)
            where T : RepoItem, new()
        {
            listToBePopulated = new List<T>();

            var returnList = new List<RepoItem>();

            using (var file = File.OpenText(jsonFilePath))
            using (var reader = new JsonTextReader(file))
            {
                var aRepoItems = JToken.ReadFrom(reader);
                var repoItemList = aRepoItems.Children();

                foreach (var jsonRepoItem in repoItemList)
                {
                    dynamic repoItem;
                    var id = jsonRepoItem["_id"];
                    var externalId = jsonRepoItem["external_id"];
                    var jsonData = jsonRepoItem.ToString();

                    if (typeof(T) == typeof(Organization))
                    {
                        repoItem = new Organization((int?) id, (Guid?) externalId, jsonData);
                        listToBePopulated.Add(repoItem);
                    }
                    else if (typeof(T) == typeof(User))
                    {
                        var organizationId = jsonRepoItem["organization_id"];
                        repoItem = new User((int?) organizationId, (int?) id, (Guid?) externalId, jsonData);
                        listToBePopulated.Add(repoItem);
                    }
                    else if (typeof(T) == typeof(Ticket))
                    {
                        var organizationId = jsonRepoItem["organization_id"];
                        var submitterId = jsonRepoItem["submitter_id"];
                        var assigneeId = jsonRepoItem["assignee_id"];

                        repoItem = new Ticket
                        {
                            Id = (Guid?) id,
                            ExternalId = (Guid?) externalId,
                            OrganizationId = (int?) organizationId,
                            SubmitterId = (int?) submitterId,
                            AssigneeId = (int?) assigneeId,
                            JsonData = jsonData
                        };

                        listToBePopulated.Add(repoItem);
                    }
                }
            }
        }

        #endregion

        #region Get Entity Details

        public Organization GetOrganizationById(int? id)
        {
            if (id.HasValue) return _organizations.FirstOrDefault(x => x.Id == id);

            return new Organization();
        }

        public ComplexUserDTO GetComplexUserById(int? userId)
        {
            var complexUser = new ComplexUserDTO();

            if (userId.HasValue)
            {
                complexUser.UserDTO = GetUserById(userId);
                complexUser.OrganizationDTO = GetOrganizationById(complexUser.UserDTO.OrganizationId);
                return complexUser;
            }

            return complexUser;
        }

        public ComplexTicketDTO GetComplexTicketById(Guid? ticketId)
        {
            var complexTicket = new ComplexTicketDTO();
            complexTicket.Ticket = GetTicketById(ticketId);

            if (complexTicket.Ticket?.Id != null)
            {
                if (complexTicket.Ticket.AssigneeId.HasValue)
                {
                    var assigneedUserId = complexTicket.Ticket.AssigneeId.Value;
                    complexTicket.TicketAssigneeDTO = GetComplexUserById(assigneedUserId);
                }

                if (complexTicket.Ticket.SubmitterId.HasValue)
                {
                    var submitterUserId = complexTicket.Ticket.SubmitterId.Value;
                    complexTicket.TicketSubmitterDTO = GetComplexUserById(submitterUserId);
                }

                if (complexTicket.Ticket.OrganizationId.HasValue)
                    complexTicket.TicketOrganizationDTO = GetOrganizationById(complexTicket.Ticket.OrganizationId);
            }

            return complexTicket;
        }

        protected User GetUserById(int? id)
        {
            if (id.HasValue) return _users.FirstOrDefault(x => x.Id == id);

            return null;
        }

        protected Ticket GetTicketById(Guid? id)
        {
            if (id.HasValue && id.Value != Guid.Empty) return _tickets.FirstOrDefault(x => x.Id == id);

            return null;
        }

        #endregion
    }
}