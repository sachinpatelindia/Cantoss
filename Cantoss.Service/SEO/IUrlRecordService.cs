using Cantoss.Domain.SEO;

namespace Cantoss.Service.SEO
{
    public interface IUrlRecordService
    {
        UrlRecord GetUrlRecordBySlug(string slug);
    }
}
