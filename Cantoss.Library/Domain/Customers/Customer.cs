using Cantoss.Library.Domain.Portals;

namespace Cantoss.Library.Domain.Customers
{
    public class Customer : CommonEntity
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(Customer);
        }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
