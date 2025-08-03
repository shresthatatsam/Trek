using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserRoles.configuration;
using UserRoles.Models;
using UserRoles.Models.Trek;

namespace UserRoles.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CarousalImage> CarousalImages { get; set; }
        public DbSet<Deals> Deals { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<NabBarContent> NavBarContents { get; set; }
        public DbSet<NavItem> NavItems { get; set; }


        public DbSet<TrekPackage> TrekPackages { get; set; }
        public DbSet<TrekOverview> TrekOverviews { get; set; }
        public DbSet<TrekItinerary> TrekItineraries { get; set; }
        public DbSet<TrekItineraryDay> TrekItineraryDays { get; set; }
        public DbSet<TrekCostInfo> TrekCostInfos { get; set; }
        public DbSet<TrekPricing> TrekPricings { get; set; }
        public DbSet<TrekPriceBreakdown> TrekPriceBreakdowns { get; set; }
        public DbSet<TrekFAQ> TrekFAQs { get; set; }
        public DbSet<TrekGalleryImage> TrekGalleryImages { get; set; }
        public DbSet<TrekInclusion> TrekInclusions { get; set; }
        public DbSet<TrekHighlight> TrekHighlights { get; set; }
        public DbSet<TrekReview> TrekReviews { get; set; }
        public DbSet<TrekDeparture> TrekDepartures { get; set; }

        // Lookup entities
        public DbSet<TrekDifficulty> TrekDifficulties { get; set; }
        public DbSet<TrekRegion> TrekRegions { get; set; }
        public DbSet<TrekSeason> TrekSeasons { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrekPackage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(250);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.ShortDescription).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Duration).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Difficulty).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MaxAltitude).HasMaxLength(50);
                entity.Property(e => e.TrekkingDistance).HasMaxLength(50);
                entity.Property(e => e.StartEndPoint).HasMaxLength(200);
                entity.Property(e => e.BestSeason).HasMaxLength(100);
                entity.Property(e => e.FeaturedImageUrl).HasMaxLength(500);
                entity.Property(e => e.MapImageUrl).HasMaxLength(500);
                entity.Property(e => e.VideoUrl).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                // One-to-One relationships
                entity.HasOne(e => e.Overview)
                    .WithOne(o => o.TrekPackage)
                    .HasForeignKey<TrekOverview>(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Itinerary)
                    .WithOne(i => i.TrekPackage)
                    .HasForeignKey<TrekItinerary>(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.CostInfo)
                    .WithOne(c => c.TrekPackage)
                    .HasForeignKey<TrekCostInfo>(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-Many relationships
                entity.HasMany(e => e.FAQs)
                    .WithOne(f => f.TrekPackage)
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.GalleryImages)
                    .WithOne(g => g.TrekPackage)
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Highlights)
                    .WithOne(h => h.TrekPackage)
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Reviews)
                    .WithOne(r => r.TrekPackage)
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Departures)
                    .WithOne(d => d.TrekPackage)
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure TrekInclusion separately to handle both Includes and Excludes
            modelBuilder.Entity<TrekInclusion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Item).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.SortOrder).HasDefaultValue(0);

                entity.HasOne(e => e.TrekPackage)
                    .WithMany()
                    .HasForeignKey(e => e.TrekPackageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // TrekOverview Configuration
            modelBuilder.Entity<TrekOverview>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
                entity.Property(e => e.ShortItinerary).HasColumnType("nvarchar(max)");
                entity.Property(e => e.ImportantNote).HasColumnType("nvarchar(max)");

                // Configure KeyPoints as JSON
                entity.Property(e => e.KeyPoints)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>())
                    .HasColumnType("nvarchar(max)");
            });

            // TrekItinerary Configuration
            modelBuilder.Entity<TrekItinerary>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Days)
                    .WithOne(d => d.TrekItinerary)
                    .HasForeignKey(e => e.TrekItineraryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // TrekItineraryDay Configuration
            modelBuilder.Entity<TrekItineraryDay>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DayNumber).IsRequired();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Altitude).HasMaxLength(50);
                entity.Property(e => e.Duration).HasMaxLength(100);
                entity.Property(e => e.Accommodation).HasMaxLength(200);
                entity.Property(e => e.Meals).HasMaxLength(200);
                entity.Property(e => e.Activities).HasMaxLength(500);
                entity.Property(e => e.Transportation).HasMaxLength(200);
            });

            // TrekCostInfo Configuration
            modelBuilder.Entity<TrekCostInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BasePrice).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Currency).IsRequired().HasMaxLength(10).HasDefaultValue("USD");
                entity.Property(e => e.PriceNote).HasMaxLength(500);

                entity.HasMany(e => e.GroupPricing)
                    .WithOne(p => p.TrekCostInfo)
                    .HasForeignKey(e => e.TrekCostInfoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Breakdown)
                    .WithOne(b => b.TrekCostInfo)
                    .HasForeignKey(e => e.TrekCostInfoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // TrekPricing Configuration
            modelBuilder.Entity<TrekPricing>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GroupSize).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PricePerPerson).HasColumnType("decimal(18,2)").IsRequired();
            });

            // TrekPriceBreakdown Configuration
            modelBuilder.Entity<TrekPriceBreakdown>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Item).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IsIncluded).IsRequired();
            });

            // TrekFAQ Configuration
            modelBuilder.Entity<TrekFAQ>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Question).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Answer).HasColumnType("nvarchar(max)");
                entity.Property(e => e.SortOrder).HasDefaultValue(0);
            });

            // TrekGalleryImage Configuration
            modelBuilder.Entity<TrekGalleryImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.AltText).HasMaxLength(200);
                entity.Property(e => e.Caption).HasMaxLength(300);
                entity.Property(e => e.IsFeatured).HasDefaultValue(false);
                entity.Property(e => e.SortOrder).HasDefaultValue(0);
            });

            // TrekHighlight Configuration
            modelBuilder.Entity<TrekHighlight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IconClass).HasMaxLength(100);
                entity.Property(e => e.SortOrder).HasDefaultValue(0);
            });

            // TrekReview Configuration
            modelBuilder.Entity<TrekReview>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReviewerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ReviewerCountry).HasMaxLength(100);
                entity.Property(e => e.ReviewText).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.ReviewDate).IsRequired();
                entity.Property(e => e.ReviewSource).HasMaxLength(100);
                entity.Property(e => e.IsVerified).HasDefaultValue(false);

                // Add check constraint for rating
                entity.HasCheckConstraint("CK_TrekReview_Rating", "[Rating] >= 1 AND [Rating] <= 5");
            });

            // TrekDeparture Configuration
            modelBuilder.Entity<TrekDeparture>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.AvailableSpots).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Available");
                entity.Property(e => e.Notes).HasMaxLength(500);

                // Add check constraint for available spots
                entity.HasCheckConstraint("CK_TrekDeparture_AvailableSpots", "[AvailableSpots] >= 0");
            });

            // Lookup entities configuration
            modelBuilder.Entity<TrekDifficulty>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Level).IsRequired();
                entity.Property(e => e.Color).HasMaxLength(20);

                // Seed data
                entity.HasData(
                    new TrekDifficulty { Id = 1, Name = "Easy", Description = "Suitable for beginners", Level = 1, Color = "#28a745" },
                    new TrekDifficulty { Id = 2, Name = "Moderate", Description = "Requires some fitness", Level = 2, Color = "#ffc107" },
                    new TrekDifficulty { Id = 3, Name = "Challenging", Description = "Requires good fitness", Level = 3, Color = "#fd7e14" },
                    new TrekDifficulty { Id = 4, Name = "Strenuous", Description = "Requires excellent fitness", Level = 4, Color = "#dc3545" },
                    new TrekDifficulty { Id = 5, Name = "Extreme", Description = "Only for experienced trekkers", Level = 5, Color = "#6f42c1" }
                );
            });

            modelBuilder.Entity<TrekRegion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                // Seed data for Nepal regions
                entity.HasData(
                    new TrekRegion { Id = 1, Name = "Everest Region", Description = "Home to the world's highest peak" },
                    new TrekRegion { Id = 2, Name = "Annapurna Region", Description = "Popular trekking destination with diverse landscapes" },
                    new TrekRegion { Id = 3, Name = "Langtang Region", Description = "Close to Kathmandu with beautiful valleys" },
                    new TrekRegion { Id = 4, Name = "Manaslu Region", Description = "Off the beaten path trekking" },
                    new TrekRegion { Id = 5, Name = "Mustang Region", Description = "Ancient kingdom with unique culture" }
                );
            });

            modelBuilder.Entity<TrekSeason>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Months).IsRequired().HasMaxLength(100);
                entity.Property(e => e.WeatherDescription).HasMaxLength(500);
                entity.Property(e => e.TemperatureRange).HasMaxLength(100);
                entity.Property(e => e.IsRecommended).HasDefaultValue(false);

                // Seed data
                entity.HasData(
                    new TrekSeason { Id = 1, Name = "Spring", Months = "March - May", WeatherDescription = "Clear skies, moderate temperatures", TemperatureRange = "10°C to 25°C", IsRecommended = true },
                    new TrekSeason { Id = 2, Name = "Monsoon", Months = "June - August", WeatherDescription = "Heavy rainfall, cloudy", TemperatureRange = "15°C to 30°C", IsRecommended = false },
                    new TrekSeason { Id = 3, Name = "Autumn", Months = "September - November", WeatherDescription = "Clear skies, perfect visibility", TemperatureRange = "5°C to 20°C", IsRecommended = true },
                    new TrekSeason { Id = 4, Name = "Winter", Months = "December - February", WeatherDescription = "Cold temperatures, clear skies", TemperatureRange = "-10°C to 15°C", IsRecommended = false }
                );
            });

            // Indexes for better performance
            modelBuilder.Entity<TrekPackage>()
                .HasIndex(t => t.IsFeatured)
                .HasDatabaseName("IX_TrekPackage_IsFeatured");

            modelBuilder.Entity<TrekPackage>()
                .HasIndex(t => t.Difficulty)
                .HasDatabaseName("IX_TrekPackage_Difficulty");

            modelBuilder.Entity<TrekPackage>()
                .HasIndex(t => t.CreatedAt)
                .HasDatabaseName("IX_TrekPackage_CreatedAt");

            modelBuilder.Entity<TrekReview>()
                .HasIndex(r => new { r.TrekPackageId, r.IsVerified })
                .HasDatabaseName("IX_TrekReview_TrekPackageId_IsVerified");

            modelBuilder.Entity<TrekDeparture>()
                .HasIndex(d => new { d.TrekPackageId, d.StartDate })
                .HasDatabaseName("IX_TrekDeparture_TrekPackageId_StartDate");
        

    
        modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
