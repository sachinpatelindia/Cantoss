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
                    Id=1,
                    EntityName="course",
                    Slug="course-abc",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    Id=2,
                    EntityName="course",
                    Slug="course-asp",
                    IsActive=true,
                },
                 new UrlRecord
                {
                    Id=3,
                    EntityName="course",
                    Slug="course-cs",
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

            return urls.FirstOrDefault(u=>u.Slug.Contains(slug));
        }
    }
}
