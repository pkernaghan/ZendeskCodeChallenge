using System;

namespace ZendeskSearchRepository.DBModels
{
    public interface IRepoItem
    {
        Guid? ExternalId { get; set; }
        string JsonData { get; set; }
    }
}