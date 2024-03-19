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
            endpointRouteBuilder.MapControllerRoute("CMS", pattern, new { controller = "CMS", action = "Details" });
            endpointRouteBuilder.MapControllerRoute("Course", pattern, new { controller = "Course", action = "Details" });
            endpointRouteBuilder.MapControllerRoute("Resume", pattern, new { controller = "Resume", action = "Loader" });
        }
    }
}
