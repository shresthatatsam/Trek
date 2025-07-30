namespace UserRoles.Models.Trek
{
    public class TrekHighlight
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public int TrekPackageId { get; set; }
    }
}
