namespace UserRoles.Models.Trek
{
    public class TrekItineraryDay
    {
        public Guid Id { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Altitude { get; set; }
        public string Duration { get; set; }
        public string Accommodation { get; set; }
        public string Meals { get; set; }
        public int TrekItineraryId { get; set; }
    }
}
