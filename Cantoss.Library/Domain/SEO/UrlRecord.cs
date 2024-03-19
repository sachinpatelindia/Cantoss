namespace Cantoss.Domain.SEO
{
    public class UrlRecord
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }
}
