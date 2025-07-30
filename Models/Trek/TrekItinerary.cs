namespace UserRoles.Models.Trek
{
    public class TrekItinerary
    {
        public Guid Id { get; set; }
        public List<TrekItineraryDay> Days { get; set; } = new List<TrekItineraryDay>();
        public int TrekPackageId { get; set; }
    }
}
