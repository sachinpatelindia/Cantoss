using Cantoss.Library;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Cantoss.Azure.Library.Cosmos
{
    public class CosmosDbHandler<T> : ICosmosDbHandler<T> where T : CommonEntity
    {
        private CosmosClient? cosmosClient;
        private Database? database;
        private Container? container;
        private readonly string endpointUri=string.Empty;
        private readonly string primaryKey = string.Empty;
        private readonly IConnectionFactory _connectionFactory;
        public CosmosDbHandler(IConnectionFactory factory)
        {
            _connectionFactory = factory;
        }

        private async Task<Container?> GetCosmosContainer()
        {
            return await CreateConnection<Container>();
        }
        public async Task<IList<T>> GetMany<T>(object partitionKey)
        {
            var container = await GetCosmosContainer();

            var sqlQueryText = "SELECT * FROM c WHERE c.partitionKey = '" + partitionKey + "'";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            List<T> data = new List<T>();
            try
            {
                FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (T family in currentResultSet)
                    {
                        data.Add(family);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return data;
        }

        public async Task<T> GetOne<T>(T entity)
        {
            dynamic? baseEntity = entity as CommonEntity;
            var container = await GetCosmosContainer();
            try
            {
                entity = await container.ReadItemAsync<T>(baseEntity.Id, new PartitionKey(baseEntity.PartitionKey)) as ItemResponse<T>;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;

        }

        public async Task<T> Insert<T>(T entity)
        {
            var container = await GetCosmosContainer();
            try
            {
                entity = await container.CreateItemAsync<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> InsertMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Modify<T>(T entity)
        {
            var container = await GetCosmosContainer();
            try
            {
                entity = await container.UpsertItemAsync<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> ModifyMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Remove<T>(T entity)
        {
            var container = await GetCosmosContainer();
            var deleteItem = entity as CommonEntity;
            PartitionKey key = new(deleteItem.PartitionKey);
            try
            {
                entity = await container.DeleteItemAsync<T>(deleteItem.Id, key);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> RemoveMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> CreateConnection<T>() where T : class
        {
            var cosmos = await CosmosDbSetup();
            return cosmos as T;
        }

        private async Task<Container?> CosmosDbSetup()
        {
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = "Cantoss Web App" });
            this.database = await cosmosClient.CreateDatabaseIfNotExistsAsync("database");
            this.container = await database.CreateContainerIfNotExistsAsync("container", "/partitionKey");
            return this.container;
        }
    }
}
