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
        public DbSet<TrekPriceBreakdown> TrekPriceBreakdowns { get; set; }
        public DbSet<TrekFAQ> TrekFAQs { get; set; }
        public DbSet<TrekGalleryImage> TrekGalleryImages { get; set; }
        public DbSet<TrekInclusion> TrekInclusions { get; set; }
        public DbSet<TrekHighlight> TrekHighlights { get; set; }
        public DbSet<TrekReview> TrekReviews { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<GuideBooking>()
            //.HasOne(gb => gb.Guide)
            //.WithMany(g => g.Bookings)
            //.HasForeignKey(gb => gb.GuideId);

            modelBuilder.Entity<NabBarContent>()
                  .HasMany(n => n.Items)
                  .WithOne(i => i.NavBarContent)
                  .HasForeignKey(i => i.NavBarContentId)
                  .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<TrekPackage>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Title)
            //          .IsRequired()
            //          .HasMaxLength(200);

            //    entity.Property(e => e.Slug)
            //          .IsRequired()
            //          .HasMaxLength(250);

            //    entity.HasIndex(e => e.Slug)
            //          .IsUnique();

            //    entity.Property(e => e.ShortDescription)
            //          .IsRequired()
            //          .HasMaxLength(500);

            //    entity.Property(e => e.Duration)
            //          .IsRequired()
            //          .HasMaxLength(50);

            //    entity.Property(e => e.Difficulty)
            //          .IsRequired()
            //          .HasMaxLength(50);

            //    entity.Property(e => e.MaxAltitude)
            //          .HasMaxLength(50);

            //    entity.Property(e => e.TrekkingDistance)
            //          .HasMaxLength(50);

            //    entity.Property(e => e.StartEndPoint)
            //          .HasMaxLength(200);

            //    entity.Property(e => e.BestSeason)
            //          .HasMaxLength(100);

            //    entity.Property(e => e.FeaturedImageUrl)
            //          .HasMaxLength(500);

            //    entity.Property(e => e.MapImageUrl)
            //          .HasMaxLength(500);
            //});

            //// Trek Overview Configuration
            //modelBuilder.Entity<TrekOverview>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Description)
            //          .IsRequired()
            //          .HasColumnType("TEXT");

            //    entity.Property(e => e.KeyPoints)
            //          .HasConversion(
            //              v => string.Join(';', v),
            //              v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            //          );

            //    entity.HasOne<TrekPackage>()
            //          .WithOne(tp => tp.Overview)
            //          .HasForeignKey<TrekOverview>(to => to.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Itinerary Configuration
            //modelBuilder.Entity<TrekItinerary>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.HasOne<TrekPackage>()
            //          .WithOne(tp => tp.Itinerary)
            //          .HasForeignKey<TrekItinerary>(ti => ti.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);

            //    entity.HasMany(ti => ti.Days)
            //          .WithOne()
            //          .HasForeignKey(tid => tid.TrekItineraryId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Itinerary Day Configuration
            //modelBuilder.Entity<TrekItineraryDay>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.DayNumber)
            //          .IsRequired();

            //    entity.Property(e => e.Title)
            //          .IsRequired()
            //          .HasMaxLength(200);

            //    entity.Property(e => e.Description)
            //          .IsRequired()
            //          .HasColumnType("TEXT");

            //    entity.Property(e => e.Altitude)
            //          .HasMaxLength(50);

            //    entity.Property(e => e.Duration)
            //          .HasMaxLength(50);

            //    entity.Property(e => e.Accommodation)
            //          .HasMaxLength(100);

            //    entity.Property(e => e.Meals)
            //          .HasMaxLength(100);

            //    // Ensure unique day numbers within each itinerary
            //    entity.HasIndex(e => new { e.TrekItineraryId, e.DayNumber })
            //          .IsUnique();
            //});

            //// Trek Cost Info Configuration
            //modelBuilder.Entity<TrekCostInfo>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Price)
            //          .IsRequired()
            //          .HasColumnType("decimal(10,2)");

            //    entity.Property(e => e.PriceNote)
            //          .HasMaxLength(500);

            //    entity.HasOne<TrekPackage>()
            //          .WithOne(tp => tp.CostInfo)
            //          .HasForeignKey<TrekCostInfo>(tci => tci.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);

            //    entity.HasMany(tci => tci.Breakdown)
            //          .WithOne()
            //          .HasForeignKey(tpb => tpb.TrekCostInfoId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Price Breakdown Configuration
            //modelBuilder.Entity<TrekPriceBreakdown>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Item)
            //          .IsRequired()
            //          .HasMaxLength(200);

            //    entity.Property(e => e.Cost)
            //          .IsRequired()
            //          .HasMaxLength(100);
            //});

            //// Trek FAQ Configuration
            //modelBuilder.Entity<TrekFAQ>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Question)
            //          .IsRequired()
            //          .HasMaxLength(500);

            //    entity.Property(e => e.Answer)
            //          .IsRequired()
            //          .HasColumnType("TEXT");

            //    entity.HasOne<TrekPackage>()
            //          .WithMany(tp => tp.FAQs)
            //          .HasForeignKey(tf => tf.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Gallery Image Configuration
            //modelBuilder.Entity<TrekGalleryImage>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.ImageUrl)
            //          .IsRequired()
            //          .HasMaxLength(500);

            //    entity.Property(e => e.AltText)
            //          .HasMaxLength(200);

            //    entity.Property(e => e.Caption)
            //          .HasMaxLength(300);

            //    entity.HasOne<TrekPackage>()
            //          .WithMany(tp => tp.GalleryImages)
            //          .HasForeignKey(tgi => tgi.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Inclusion Configuration
            //modelBuilder.Entity<TrekInclusion>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Item)
            //          .IsRequired()
            //          .HasMaxLength(200);

            //    entity.Property(e => e.Description)
            //          .HasMaxLength(500);

            //    entity.HasOne<TrekPackage>()
            //          .WithMany(tp => tp.Includes)
            //          .HasForeignKey(ti => ti.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);

            //    // Note: You might want to separate Includes and Excludes into different navigation properties
            //    // or use a discriminator column if you want to keep them in the same table
            //});

            //// Trek Highlight Configuration
            //modelBuilder.Entity<TrekHighlight>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Title)
            //          .IsRequired()
            //          .HasMaxLength(100);

            //    entity.Property(e => e.Description)
            //          .HasMaxLength(500);

            //    entity.Property(e => e.IconClass)
            //          .HasMaxLength(100);

            //    entity.HasOne<TrekPackage>()
            //          .WithMany(tp => tp.Highlights)
            //          .HasForeignKey(th => th.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            //// Trek Review Configuration
            //modelBuilder.Entity<TrekReview>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.ReviewerName)
            //          .IsRequired()
            //          .HasMaxLength(100);

            //    entity.Property(e => e.ReviewText)
            //          .IsRequired()
            //          .HasColumnType("TEXT");

            //    entity.Property(e => e.Rating)
            //          .IsRequired()
            //          .HasDefaultValue(5);

            //    entity.Property(e => e.ReviewDate)
            //          .IsRequired()
            //          .HasDefaultValueSql("GETDATE()");

            //    entity.HasOne<TrekPackage>()
            //          .WithMany(tp => tp.Reviews)
            //          .HasForeignKey(tr => tr.TrekPackageId)
            //          .OnDelete(DeleteBehavior.Cascade);

            //    // Ensure rating is between 1 and 5
            //    entity.HasCheckConstraint("CK_TrekReview_Rating", "[Rating] >= 1 AND [Rating] <= 5");
            //});

            //// Additional indexes for performance
            //modelBuilder.Entity<TrekPackage>()
            //          .HasIndex(e => e.IsFeatured);

            //modelBuilder.Entity<TrekPackage>()
            //          .HasIndex(e => e.Difficulty);

            //modelBuilder.Entity<TrekPackage>()
            //          .HasIndex(e => e.Duration);

            //modelBuilder.Entity<TrekReview>()
            //          .HasIndex(e => e.Rating);

            //modelBuilder.Entity<TrekReview>()
            //          .HasIndex(e => e.ReviewDate);

            modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
