using Cantoss.Azure.Library.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cantoss.Azure.Library
{
    /// <summary>
    /// Azure connection manager manages to connect different azure services
    /// </summary>
    public static class CosmosDbDependencyRegistrar
    {
        private static Connection? _connection;
        public static void AddCosmosDbDependencyRegistrar(this IServiceCollection services, IConfiguration configuration)
        {
            _connection = GetConnection(services, configuration);
            RegisterDependency(services);
        }

        private static void RegisterDependency(IServiceCollection services)
        {
            _ = services.AddScoped<IConnectionFactory, ConnectionFactory>(con =>
            {
                return new ConnectionFactory(_connection);
            });
            services.AddScoped(typeof(ICosmosDbHandler<>), typeof(CosmosDbHandler<>));
        }

        private static Connection GetConnection(IServiceCollection services, IConfiguration configuration)
        {
            var connection = new Connection();
            configuration.GetSection("Azure").Bind(connection);
            return connection;
        }
    }
}
