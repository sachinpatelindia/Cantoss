using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Cantoss.Web.Framework.MVC.Routing
{
    public class SlugRouteTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values == null)
                return new ValueTask<RouteValueDictionary>(values);
            if (!values.TryGetValue("SeName", out var slugValues) || string.IsNullOrEmpty(slugValues as string))
                return new ValueTask<RouteValueDictionary>(values);
            var slug = slugValues as string;
            if (slug == null)
                return new ValueTask<RouteValueDictionary>(values);
            var urlRecord = GetUrlRecords().FirstOrDefault(v => v.Slug.Contains(slug));

            if (urlRecord == null)
                return new ValueTask<RouteValueDictionary>(values);
            if (!urlRecord.IsActive)
                return new ValueTask<RouteValueDictionary>(values);

            switch (urlRecord.EntityName.ToLowerInvariant())
            {
                case "learn":
                    values[RoutePathDefaults.ControllerFieldKey] = "learn";
                    values[RoutePathDefaults.ActionFieldKey] = "details";
                    values[RoutePathDefaults.LearnFieldKey] = urlRecord.Id;
                    values[RoutePathDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;

                case "resume":
                    values[RoutePathDefaults.ControllerFieldKey] = "resume";
                    values[RoutePathDefaults.ActionFieldKey] = "loader";
                    values[RoutePathDefaults.ResumeFieldKey] = urlRecord.Id;
                    values[RoutePathDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                default:
                    break;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }

        private List<UrlRecord> GetUrlRecords()
        {
            return new List<UrlRecord>
            {
                new UrlRecord
                {
                    Id=1,
                    EntityName="Learn",
                    Slug="learn-abc",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    Id=2,
                    EntityName="Learn",
                    Slug="learn-asp",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    Id=3,
                    EntityName="Learn",
                    Slug="learn-cs",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    Id=4,
                    EntityName="Resume",
                    Slug="resume-builder",
                    IsActive=true,
                }
            };

        }
    }
}


public class UrlRecord
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string Slug { get; set; }
    public bool IsActive { get; set; }
}