namespace UserRoles.Models.Trek
{
    public class TrekReview
    {
        public Guid Id { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; } // 1-5
        public DateTime ReviewDate { get; set; }
        public int TrekPackageId { get; set; }
    }
}
