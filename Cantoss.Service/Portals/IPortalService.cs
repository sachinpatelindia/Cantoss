using Cantoss.Library.Domain.Portals;

namespace Cantoss.Service.Portals
{
    public interface IPortalService
    {
        Portal GetPortalById(int? portalId = null);
    }
}
