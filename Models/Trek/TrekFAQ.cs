namespace UserRoles.Models.Trek
{
    public class TrekFAQ
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int TrekPackageId { get; set; }
    }
}
