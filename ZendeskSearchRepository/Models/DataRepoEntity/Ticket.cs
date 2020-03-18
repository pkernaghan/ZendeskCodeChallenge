using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.DBModels
{
    public class Ticket: RepoItem
    {
        public Guid? Id { get; set; }
        public int? OrganizationId { get; set; }
        public int? SubmitterId { get; set; }
        public int? AssigneeId { get; set; }

        public Ticket()
        {

        }

        public Ticket(int? assigneeId, int? submitterId, int? organizationId, Guid? id, Guid? externalId, string jsonData) : base(externalId, jsonData)
        {
            OrganizationId = organizationId;
            SubmitterId = submitterId;
            AssigneeId = assigneeId;
            Id = id;
        }
    }
}
