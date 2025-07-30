namespace UserRoles.Models.Trek
{
    public class TrekPackage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Duration { get; set; }
        public string Difficulty { get; set; }
        public string MaxAltitude { get; set; }
        public string TrekkingDistance { get; set; }
        public string StartEndPoint { get; set; }
        public string BestSeason { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string MapImageUrl { get; set; }
        public bool IsFeatured { get; set; }

        // Relationships
        public TrekOverview Overview { get; set; }
        public TrekItinerary Itinerary { get; set; }
        public TrekCostInfo CostInfo { get; set; }
        public List<TrekFAQ> FAQs { get; set; } = new List<TrekFAQ>();
        public List<TrekGalleryImage> GalleryImages { get; set; } = new List<TrekGalleryImage>();
        public List<TrekInclusion> Includes { get; set; } = new List<TrekInclusion>();
        public List<TrekInclusion> Excludes { get; set; } = new List<TrekInclusion>();
        public List<TrekHighlight> Highlights { get; set; } = new List<TrekHighlight>();
        public List<TrekReview> Reviews { get; set; } = new List<TrekReview>();
    }
}
