using Newtonsoft.Json;
namespace Cantoss.Library
{
    public abstract class CommonEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
    }
}
