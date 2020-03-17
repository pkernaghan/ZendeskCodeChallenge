using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;

namespace ZendeskSearchRepository.DBModels
{
    public class Organization: RepoItem
    {
        public int? Id { get; set; }

        public Organization()
        {

        }

        public Organization(int? id, Guid? externalId, string jsonData) : base(externalId, jsonData)
        {
            Id = id;
        }
    }
}
