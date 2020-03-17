namespace ZendeskSearchRepository.Configuration
{
    public interface ISearchRepoConfigSettings
    {
        string OrganizationsFilePath { get; }
        string TicketsFilePath { get;  }
        string UsesFilePath { get; }
    }
}