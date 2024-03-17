using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public interface IRouteProvider
    {
        void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder);
        int Priority { get; }
    }
}
