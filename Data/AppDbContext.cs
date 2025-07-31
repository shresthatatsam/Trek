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


            modelBuilder.Entity<TrekPackage>()
             .HasMany(p => p.Includes)
             .WithOne()
             .HasForeignKey(i => i.TrekPackageId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrekPackage>()
                .HasMany(p => p.Excludes)
                .WithOne()
                .HasForeignKey(i => i.TrekPackageId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
