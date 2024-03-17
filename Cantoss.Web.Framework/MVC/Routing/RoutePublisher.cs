using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public class RoutePublisher : IRoutePublisher
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var type = typeof(IRouteProvider);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(at => at.GetTypes())
                .Where(t => type.IsAssignableFrom(t) && !t.IsInterface);

            var instances = types.Select(t => (IRouteProvider)Activator.CreateInstance(t))
                .OrderByDescending(t => t.Priority);

            foreach (var instance in instances)
            {
                instance.RegisterRoutes(endpointRouteBuilder);
            }
        }
    }
}
