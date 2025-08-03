namespace UserRoles.Models.Trek
{
    using System.ComponentModel.DataAnnotations;

    // Main Trek Package Entity
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
        public string VideoUrl { get; set; } 
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

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
        public List<TrekDeparture> Departures { get; set; } = new List<TrekDeparture>();
    }

    public class TrekOverview
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ShortItinerary { get; set; }
        public string ImportantNote { get; set; }
        public List<string> KeyPoints { get; set; } = new List<string>();
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekItinerary
    {
        public int Id { get; set; }
        public List<TrekItineraryDay> Days { get; set; } = new List<TrekItineraryDay>();
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekItineraryDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Altitude { get; set; }
        public string Duration { get; set; }
        public string Accommodation { get; set; }
        public string Meals { get; set; }
        public string Activities { get; set; }
        public string Transportation { get; set; }
        public int TrekItineraryId { get; set; }
        public TrekItinerary TrekItinerary { get; set; }
    }

    public class TrekCostInfo
    {
        public int Id { get; set; }
        public decimal BasePrice { get; set; }
        public string Currency { get; set; } = "USD";
        public string PriceNote { get; set; }
        public List<TrekPricing> GroupPricing { get; set; } = new List<TrekPricing>();
        public List<TrekPriceBreakdown> Breakdown { get; set; } = new List<TrekPriceBreakdown>();
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekPricing
    {
        public int Id { get; set; }
        public string GroupSize { get; set; } // e.g., "1 Pax", "2-3 Pax"
        public decimal PricePerPerson { get; set; }
        public int TrekCostInfoId { get; set; }
        public TrekCostInfo TrekCostInfo { get; set; }
    }

    public class TrekPriceBreakdown
    {
        public int Id { get; set; }
        public string Category { get; set; } // e.g., "Accommodation", "Meals", "Transportation"
        public string Item { get; set; }
        public string Description { get; set; }
        public bool IsIncluded { get; set; }
        public int TrekCostInfoId { get; set; }
        public TrekCostInfo TrekCostInfo { get; set; }
    }

    public class TrekFAQ
    {
        public int Id { get; set; }
        public string Category { get; set; } // e.g., "General", "Physical Fitness", "Weather"
        public string Question { get; set; }
        public string Answer { get; set; }
        public int SortOrder { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekGalleryImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public bool IsFeatured { get; set; }
        public int SortOrder { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekInclusion
    {
        public int Id { get; set; }
        public string Category { get; set; } // e.g., "Accommodation", "Meals", "Transportation"
        public string Item { get; set; }
        public string Description { get; set; }
        public bool IsIncluded { get; set; } // true for includes, false for excludes
        public int SortOrder { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekHighlight
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public int SortOrder { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekReview
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerCountry { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; } // 1-5
        public DateTime ReviewDate { get; set; }
        public string ReviewSource { get; set; } // e.g., "TripAdvisor", "Google"
        public bool IsVerified { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    public class TrekDeparture
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSpots { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } // "Available", "Limited", "Sold Out"
        public string Notes { get; set; }
        public int TrekPackageId { get; set; }
        public TrekPackage TrekPackage { get; set; }
    }

    // Additional entities for better organization
    public class TrekDifficulty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; } // 1-5
        public string Color { get; set; }
    }

    public class TrekRegion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class TrekSeason
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Months { get; set; }
        public string WeatherDescription { get; set; }
        public string TemperatureRange { get; set; }
        public bool IsRecommended { get; set; }
    }

   
}
