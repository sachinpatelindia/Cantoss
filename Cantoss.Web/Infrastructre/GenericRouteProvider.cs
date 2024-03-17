using Cantoss.Web.Framework.MVC.Routing;

namespace Cantoss.Web.Infrastructre
{
    public class GenericRouteProvider : IRouteProvider
    {
        public int Priority => -1000;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            string pattern = "{SeName}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(pattern);
            endpointRouteBuilder.MapControllerRoute("Article",pattern,new {controller ="Articles",action = "Details" });
            endpointRouteBuilder.MapControllerRoute("Learn", pattern, new { controller = "Learn", action = "Details" });
        }
    }
}
