using Cantoss.Azure.Library.Cosmos;
using Cantoss.Domain.SEO;
using Cantoss.Library;
using System.Text;

namespace Cantoss.Service.SEO
{
    public class UrlRecordService : IUrlRecordService
    {

        private readonly ICosmosDbHandler<UrlRecord> _urlRecordStorage;

        public virtual async Task<IList<UrlRecord>> UrlRecordsByPartitionKey(object partitionKey) => await _urlRecordStorage.GetMany<UrlRecord>(partitionKey);
        public virtual IEnumerable<UrlRecord> UrlRecords { get; }
        public UrlRecordService(ICosmosDbHandler<UrlRecord> urlRecordStorage)
        {
            _urlRecordStorage = urlRecordStorage;
        }
        public virtual string GetSeName(string entityRowKey, string entityName, bool returnDefaultValue = true)
        {
            var result = string.Empty;
            if (string.IsNullOrEmpty(result) && returnDefaultValue)
                result = GetActiveSlug(entityRowKey, entityName);
            return result;
        }

        private string GetActiveSlug(string entityRowKey, string entityName)
        {
            var urlRecords = from ur in UrlRecords
                             where ur.EntityId == entityRowKey &&
                             ur.EntityName == entityName &&
                             ur.IsActive
                             orderby ur.EntityName descending
                             select ur.Slug;

            var slug = urlRecords.FirstOrDefault() ?? string.Empty;
            return slug;
        }


        public virtual string GetSeName(string name, bool allowUniCodeCharsInUrls)
        {
            if (string.IsNullOrEmpty(name))
                return name;
            var okChars = "abcdefghijklmnopqrstuvwxyz1234567890 _-";
            name = name.Trim();


            var sb = new StringBuilder();
            foreach (var c in name.ToCharArray())
            {
                var c2 = c.ToString();
                if (allowUniCodeCharsInUrls)
                {
                    if (Char.IsLetterOrDigit(c) || okChars.Contains(c2))
                        sb.Append(c2);
                }
                else if (okChars.Contains(c2))
                {
                    sb.Append(c2);
                }
            }

            var name2 = sb.ToString();
            name2 = name2.Replace(" ", "-");
            while (name2.Contains("--"))
                name2 = name2.Replace("--", "-");
            while (name2.Contains("__"))
                name2 = name2.Replace("__", "_");
            return name2;
        }

        public string GetSeName<T>(T entity, bool returnDefaultValue = true) where T : CommonEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var entityName = entity.GetType().Name;
            return GetSeName(entity.Id, entityName, returnDefaultValue);
        }

        public virtual UrlRecord GetBySlug(string slug)
        {
            var urlRecords = from ur in UrlRecords
                             where ur.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)
                             orderby ur.IsActive descending, ur.Id
                             select ur;
            var urlRecord = urlRecords.FirstOrDefault();
            return urlRecord;
        }

        public virtual void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord is null)
                throw new ArgumentNullException(nameof(urlRecord));
            _urlRecordStorage.Insert(urlRecord);
        }

        public virtual void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord is null)
                throw new ArgumentNullException(nameof(urlRecord));
            _urlRecordStorage.Modify(urlRecord);
        }

        public virtual void DeleteUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord is null)
                throw new ArgumentNullException(nameof(urlRecord));
            _urlRecordStorage.Remove(urlRecord);
        }

        public virtual void SaveSlug<T>(T entity, string slug) where T : CommonEntity
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            var entityId = entity.Id;
            var entityName = entity.GetType().Name;

            var query = from ur in UrlRecords
                        where ur.Id == entityId &&
                        ur.EntityName == entityName
                        orderby ur.Id descending
                        select ur;

            var urlRecords = query.ToList();
            var activeRecord = urlRecords.FirstOrDefault(u => u.IsActive);
            UrlRecord nonActiveUrlRecord;

            if (activeRecord == null && !string.IsNullOrWhiteSpace(slug))
            {
                nonActiveUrlRecord = urlRecords
                    .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                if (nonActiveUrlRecord != null)
                {
                    nonActiveUrlRecord.IsActive = true;
                    UpdateUrlRecord(nonActiveUrlRecord);
                }
                else
                {
                    var urlRecord = new UrlRecord
                    {
                        EntityId = entityId,
                        EntityName = entityName,
                        Slug = slug,
                        IsActive = true
                    };
                    InsertUrlRecord(urlRecord);

                }
            }

            if (activeRecord != null && string.IsNullOrWhiteSpace(slug))
            {
                activeRecord.IsActive = false;
                UpdateUrlRecord(activeRecord);
            }

            if (activeRecord == null || string.IsNullOrWhiteSpace(slug))
                return;

            if (activeRecord.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                return;

            nonActiveUrlRecord = urlRecords
                .FirstOrDefault(u => u.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !u.IsActive);

            if (nonActiveUrlRecord != null)
            {
                nonActiveUrlRecord.IsActive = true;
                UpdateUrlRecord(nonActiveUrlRecord);

                activeRecord.IsActive = false;
                UpdateUrlRecord(activeRecord);
            }
            else
            {
                var urlRecord = new UrlRecord
                {
                    EntityId = entityId,
                    EntityName = entityName,
                    Slug = slug,
                    IsActive = true
                };
                InsertUrlRecord(urlRecord);

                activeRecord.IsActive = false;
                UpdateUrlRecord(activeRecord);
            }
        }

        public virtual string ValidateSeName<T>(T entity, string seName, string name, bool ensureNotEmpty) where T : CommonEntity
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            var entityName = entity.GetType().Name;
            return ValidateSeName(entity.Id, entityName, seName, name, ensureNotEmpty);
        }

        public virtual string ValidateSeName(string entityRowKey, string entityName, string seName, string name, bool ensureNotEmpty)
        {
            if (string.IsNullOrWhiteSpace(seName) && !string.IsNullOrWhiteSpace(name))
                seName = name;
            seName = GetSeName(seName, true);

            if (string.IsNullOrWhiteSpace(seName))
            {
                if (ensureNotEmpty)
                {
                    seName = entityRowKey;
                }
                else
                {
                    return seName;
                }
            }
            int i = 2;
            var tempName = seName;
            while (true)
            {
                var urlRecord = GetBySlug(tempName);
                var reserved = urlRecord != null && !(urlRecord.EntityId == entityRowKey && urlRecord.EntityName.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
                if (!reserved)
                    break;
                tempName = $"{seName}-{i}";
                i++;
            }
            seName = tempName;
            return seName;
        }


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
