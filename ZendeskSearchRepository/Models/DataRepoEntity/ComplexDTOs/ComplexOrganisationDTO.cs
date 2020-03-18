using ZendeskSearchRepository.DBModels;

namespace ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs
{
    // Note - for now this is essentially a decorator class to keep consistency with the naming and use of the other entity types i.e. tickets and users
    // It can of course be removed, or extended if necessary.
    public class ComplexOrganisationDTO: Organization
    {
        public ComplexOrganisationDTO() : base()
        {

        }
        public ComplexOrganisationDTO(Organization org) 
        {
            this.Id = org.Id;
            this.ExternalId = org.ExternalId;
            this.JsonData = org.JsonData;
        }

    }
}
