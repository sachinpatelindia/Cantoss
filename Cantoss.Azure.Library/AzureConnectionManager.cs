using Microsoft.Extensions.Configuration;

namespace Cantoss.Azure.Library
{
    public sealed class AzureConnectionManager
    {
        private static AzureConnectionManager? connectionManager = null;
        public string? ConnectionString { get; private set; }
        public string? PrimaryKey { get; private set; }
        private AzureConnectionManager() { }

        public static AzureConnectionManager Instance(IConfiguration configuration)
        {

            if (connectionManager == null)
            {
                connectionManager = new AzureConnectionManager();
                connectionManager.ConnectionString = configuration.GetSection("cosmos_con").Value.ToString();
                connectionManager.PrimaryKey = configuration.GetSection("consmos_primaryKey").Value.ToString();
            }
            return connectionManager;
        }
    }
}
