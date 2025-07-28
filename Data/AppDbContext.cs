using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserRoles.configuration;
using UserRoles.Models;

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


            modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
