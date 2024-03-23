using Cantoss.Library.Domain.Portals;

namespace Cantoss.Service.Portals
{
    public class PortalService : IPortalService
    {
        public Portal GetPortalById(int? portalId = null)
        {
            return new Portal
            {
                Description = "This is portal info page, it contains information about portal contact details.",
                Domain = "www.cantoss.com",
                Email = "contact@example.com",
                Id = 1,
                IsActive = true,
                Name = "Cantoss web",
                Phone = "1234567890"
            };
        }
    }
}
