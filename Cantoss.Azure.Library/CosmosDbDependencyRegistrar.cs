using Cantoss.Azure.Library.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cantoss.Azure.Library
{
    /// <summary>
    /// Azure connection manager manages to connect different azure services
    /// </summary>
    public class CosmosDbDependencyRegistrar
    {
        private Connection? _connection;
        public CosmosDbDependencyRegistrar(IServiceCollection services, IConfiguration configuration)
        {
            _connection = this.GetConnection(services, configuration);
            this.RegisterDependency(services);
        }

        private void RegisterDependency(IServiceCollection services)
        {
            _ = services.AddScoped<IConnectionFactory, ConnectionFactory>(con =>
            {
                return new ConnectionFactory(_connection);
            });
            services.AddScoped(typeof(ICosmosDbHandler<>), typeof(CosmosDbHandler<>));
        }

        private Connection GetConnection(IServiceCollection services, IConfiguration configuration)
        {
            var connection = new Connection();
            configuration.GetSection("Azure").Bind(connection);
            return connection;
        }
    }
}
