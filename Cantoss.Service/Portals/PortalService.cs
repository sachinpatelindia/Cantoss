using Cantoss.Azure.Library.Cosmos;
using Cantoss.Library.Domain.Portals;

namespace Cantoss.Service.Portals
{
    public class PortalService : IPortalService
    {
        private readonly ICosmosDbHandler<Portal> _dbHandler;
        public PortalService(ICosmosDbHandler<Portal> dbHandler)
        {
            _dbHandler = dbHandler;
        }
        public async Task<Portal> GetPortalById(int? portalId = null)
        {
            var portal =await _dbHandler.GetMany<Portal>(nameof(Portal));
            return portal.FirstOrDefault();
        }
    }
}
