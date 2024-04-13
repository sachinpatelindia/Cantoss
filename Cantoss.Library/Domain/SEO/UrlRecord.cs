using Cantoss.Library;

namespace Cantoss.Domain.SEO
{
    public class UrlRecord:CommonEntity
    {
        public UrlRecord()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(UrlRecord);
        }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }
}
