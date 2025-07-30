namespace UserRoles.Models.Trek
{
    public class TrekPriceBreakdown
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public string Cost { get; set; }
        public bool Included { get; set; }
        public int TrekCostInfoId { get; set; }
    }
}
