using Cantoss.Domain.SEO;
using Cantoss.Library;

namespace Cantoss.Service.SEO
{
    public interface IUrlRecordService
    {
        UrlRecord GetUrlRecordBySlug(string slug);
        string GetSeName<T>(T entity, bool returnDefaultValue = true) where T : CommonEntity;
        string GetSeName(string entityRowKey, string entityName, bool returnDefaultValue = true);
        string GetSeName(string name, bool allowUniCodeCharsInUrls);
        void InsertUrlRecord(UrlRecord urlRecord);
        void UpdateUrlRecord(UrlRecord urlRecord);
        void DeleteUrlRecord(UrlRecord urlRecord);
        void SaveSlug<T>(T entity, string slug) where T : CommonEntity;
        string ValidateSeName<T>(T entity, string seName, string name, bool ensureNotEmpty) where T : CommonEntity;
        string ValidateSeName(string entityRowKey, string entityName, string seName, string name, bool ensureNotEmpty);
        Task<IList<UrlRecord>> UrlRecordsByPartitionKey(object partitionKey);
        IEnumerable<UrlRecord> UrlRecords { get; }
        UrlRecord GetBySlug(string slug);
    }
}
