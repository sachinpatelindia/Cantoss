using System.Net;

namespace Cantoss.Library.Domain.CMS
{
    public class Cms:CommonEntity
    {
        public Cms()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(Cms);
        }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsPUblished { get; set; }
    }
}
