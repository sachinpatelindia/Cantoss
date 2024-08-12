namespace Cantoss.Azure.Library
{
    public class Connection
    {
        public CosmosDb? CosmosDb { get; set; }
    }

    public class CosmosDb
    {
        public required string EndpointUri { get; set; }
        public required string PrimaryKey { get; set; }
    }
}
