using Cantoss.Library.Domain.Portals;

namespace Cantoss.Library.Domain.Hires
{
    public class Hire:CommonEntity
    {
        public Hire()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(Portal);
        }
        public bool IsAlive { get; set; }

    }
}
