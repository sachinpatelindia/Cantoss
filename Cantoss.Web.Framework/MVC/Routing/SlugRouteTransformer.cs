using Cantoss.Service.SEO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public class SlugRouteTransformer : DynamicRouteValueTransformer
    {
        private readonly IUrlRecordService _urlRecordService;
        public SlugRouteTransformer(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values == null)
                return new ValueTask<RouteValueDictionary>(values);
            if (!values.TryGetValue("SeName", out var slugValues) || string.IsNullOrEmpty(slugValues as string))
                return new ValueTask<RouteValueDictionary>(values);
            var slug = slugValues as string;
            if (slug == null)
                return new ValueTask<RouteValueDictionary>(values);
            var urlRecord = _urlRecordService.GetUrlRecordBySlug(slug);

            if (urlRecord == null)
                return new ValueTask<RouteValueDictionary>(values);
            if (!urlRecord.IsActive)
                return new ValueTask<RouteValueDictionary>(values);

            switch (urlRecord.EntityName.ToLowerInvariant())
            {
                case "course":
                    values[RoutePathDefaults.ControllerFieldKey] = "course";
                    values[RoutePathDefaults.ActionFieldKey] = "details";
                    values[RoutePathDefaults.CourseFieldKey] = urlRecord.EntityId;
                    values[RoutePathDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;

                case "hires":
                    values[RoutePathDefaults.ControllerFieldKey] = "hires";
                    values[RoutePathDefaults.ActionFieldKey] = "loader";
                    values[RoutePathDefaults.HireFieldKey] = urlRecord.EntityId;
                    values[RoutePathDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;

                case "cms":
                    values[RoutePathDefaults.ControllerFieldKey] = "cms";
                    values[RoutePathDefaults.ActionFieldKey] = "details";
                    values[RoutePathDefaults.CmsIdFieldKey] = urlRecord.EntityId;
                    values[RoutePathDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;

                default:
                    break;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}