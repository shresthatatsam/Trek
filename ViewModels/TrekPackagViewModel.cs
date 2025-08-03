using System.ComponentModel.DataAnnotations;
using UserRoles.Models.Trek;

namespace UserRoles.ViewModels
{

    public class TrekPackageCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        [Required]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Difficulty { get; set; }

        public string MaxAltitude { get; set; }
        public string TrekkingDistance { get; set; }
        public string StartEndPoint { get; set; }
        public string BestSeason { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string MapImageUrl { get; set; }
        public string VideoUrl { get; set; } = "abc";
        public bool IsFeatured { get; set; }

        // Overview
        public string OverviewDescription { get; set; }
        public string ShortItinerary { get; set; } = "abc";
        public string ImportantNote { get; set; } = "test";
        public List<string> OverviewKeyPoints { get; set; } = new List<string>();

        // Cost Info
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than 0")]
        public decimal BasePrice { get; set; }
        public string Currency { get; set; } = "USD";
        public string PriceNote { get; set; }
        public List<GroupPricingViewModel> GroupPricing { get; set; } = new List<GroupPricingViewModel>();
        public List<PriceBreakdownViewModel> PriceBreakdown { get; set; } = new List<PriceBreakdownViewModel>();

        // Itinerary
        public List<ItineraryDayViewModel> ItineraryDays { get; set; } = new List<ItineraryDayViewModel>();

        // FAQs
        public List<FAQViewModel> FAQs { get; set; } = new List<FAQViewModel>();

        // Gallery
        public List<GalleryImageViewModel> GalleryImages { get; set; } = new List<GalleryImageViewModel>();

        // Includes/Excludes
        public List<InclusionViewModel> Includes { get; set; } = new List<InclusionViewModel>();
        public List<InclusionViewModel> Excludes { get; set; } = new List<InclusionViewModel>();

        // Highlights
        public List<HighlightViewModel> Highlights { get; set; } = new List<HighlightViewModel>();

        // Departures
        public List<DepartureViewModel> Departures { get; set; } = new List<DepartureViewModel>();
    }

    public class ItineraryDayViewModel
    {
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Altitude { get; set; }
        public string Duration { get; set; }
        public string Accommodation { get; set; }
        public string Meals { get; set; }
        public string Activities { get; set; }
        public string Transportation { get; set; }
    }

    public class GroupPricingViewModel
    {
        public string GroupSize { get; set; }
        public decimal PricePerPerson { get; set; }
    }

    public class PriceBreakdownViewModel
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public bool IsIncluded { get; set; }
    }

    public class FAQViewModel
    {
        public string Category { get; set; } = "abc";
        public string Question { get; set; }
        public string Answer { get; set; }
        public int SortOrder { get; set; }
    }

    public class GalleryImageViewModel
    {
        public string ImageUrl { get; set; } = "abc";
        public string AltText { get; set; } = "abc";
        public string Caption { get; set; } = "abc";
        public bool IsFeatured { get; set; }
        public int SortOrder { get; set; }
    }

    public class InclusionViewModel
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }

    public class HighlightViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public int SortOrder { get; set; }
    }

    public class DepartureViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(1, 50)]
        public int AvailableSpots { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string Status { get; set; } = "Available";
        public string Notes { get; set; }
    }

    // Display ViewModel for showing trek packages
    public class TrekPackageDisplayViewModel
    {
        public TrekPackage TrekPackage { get; set; }
        public Dictionary<string, List<TrekFAQ>> FAQsByCategory { get; set; }
        public Dictionary<string, List<TrekInclusion>> IncludesByCategory { get; set; }
        public Dictionary<string, List<TrekInclusion>> ExcludesByCategory { get; set; }
        public List<TrekReview> LatestReviews { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public List<TrekDeparture> AvailableDepartures { get; set; }
    }

}
