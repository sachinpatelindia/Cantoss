using Cantoss.Library.Domain.Portals;

namespace Cantoss.Service.Portals
{
    public interface IPortalService
    {
        Task<Portal> GetPortalById(int? portalId = null);
    }
}
