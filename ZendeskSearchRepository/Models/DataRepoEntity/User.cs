using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.DBModels
{
    public class User: RepoItem, IUser
    {
        public int? Id { get; set; }
        public int? OrganizationId { get; set; }

        public User() { }

        public User(int? organizationId, int? id, Guid? externalId, string jsonData) : base(externalId, jsonData)
        {
            Id = id;
            OrganizationId = organizationId;
        }
    }
}
