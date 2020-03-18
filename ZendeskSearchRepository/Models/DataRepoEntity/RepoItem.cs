using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.DBModels
{
    public class RepoItem: IRepoItem
    {
        public Guid? ExternalId { get; set; }
        public string JsonData { get; set; }

        public RepoItem()
        {

        }

        public RepoItem(Guid? externalId, string jsonData)
        {
            ExternalId = externalId;
            JsonData = jsonData;
        }

    }
}
