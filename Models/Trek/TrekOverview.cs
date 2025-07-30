namespace UserRoles.Models.Trek
{
    public class TrekOverview
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<string> KeyPoints { get; set; } = new List<string>();
        public int TrekPackageId { get; set; }
    }
}
