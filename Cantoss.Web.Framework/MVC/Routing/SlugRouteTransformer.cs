using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public class SlugRouteTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}
