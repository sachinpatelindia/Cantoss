using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public interface IRoutePublisher
    {
        void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder);
    }
}
