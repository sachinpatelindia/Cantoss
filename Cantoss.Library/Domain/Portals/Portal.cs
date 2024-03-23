namespace Cantoss.Library.Domain.Portals
{
    public class Portal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
    }
}
