namespace UserRoles.Models.Trek
{
    public class TrekCostInfo
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string PriceNote { get; set; }
        public List<TrekPriceBreakdown> Breakdown { get; set; } = new List<TrekPriceBreakdown>();
        public int TrekPackageId { get; set; }
    }
}
