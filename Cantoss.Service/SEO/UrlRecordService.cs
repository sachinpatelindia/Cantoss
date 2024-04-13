using Cantoss.Domain.SEO;

namespace Cantoss.Service.SEO
{
    public class UrlRecordService : IUrlRecordService
    {
        public UrlRecord GetUrlRecordBySlug(string slug)
        {
           var urls=new List<UrlRecord>
            {
                new UrlRecord
                {
                    EntityName="course",
                    EntityId = "a1888f63-9e97-4a6c-aa8d-920a19fc34a3",
                    Slug="course-abc",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    EntityName="course",
                    Slug="course-asp",
                    EntityId = "a1888f63-9e97-4a6c-aa8d-920a19fc34a2",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    EntityName="course",
                     EntityId = "a1888f63-9e97-4a6c-aa8d-920a19fc34a1",
                    Slug="course-cs",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    EntityName="hires",
                     EntityId = "a1888f63-9e97-4a6c-aa8d-920a19fc34a0",
                    Slug="hire-me",
                    IsActive=true,
                }
            };

            return urls.FirstOrDefault(u=>u.Slug.Contains(slug));
        }
    }
}
