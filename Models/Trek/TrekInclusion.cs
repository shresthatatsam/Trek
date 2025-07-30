namespace UserRoles.Models.Trek
{
    public class TrekInclusion
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public bool IsIncluded { get; set; } // true for includes, false for excludes
        public int TrekPackageId { get; set; }
    }
}
