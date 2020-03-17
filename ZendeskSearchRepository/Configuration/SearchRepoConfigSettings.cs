using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;
using ZendeskSearchRepository.Constants;

namespace ZendeskSearchRepository.Configuration
{
    public class SearchRepoConfigSettings : ISearchRepoConfigSettings
    {
        #region Properties

        public string OrganizationsFilePath { get; protected set; }
        public string TicketsFilePath { get; protected set; }
        public string UsesFilePath { get; protected set; }

        #endregion

        #region Constructor

        public SearchRepoConfigSettings(IConfiguration configuration)
        {
            OrganizationsFilePath = configuration.GetValue<string>(ConfigSettings.ConfigFileProperty.OrganizationsFilePath);
            TicketsFilePath = configuration.GetValue<string>(ConfigSettings.ConfigFileProperty.TicketsFilePath);
            UsesFilePath = configuration.GetValue<string>(ConfigSettings.ConfigFileProperty.UsersFilePath);
        }

        #endregion
    }
}



