using Cantoss.Domain.SEO;

namespace Cantoss.Library.Domain.Portals
{
    public class Portal:CommonEntity
    {
        public Portal()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(Portal);
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
    }
}
