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
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<TeamMember>()
               .HasOne(tm => tm.AboutUsSection)
               .WithMany(a => a.TeamMembers)
               .HasForeignKey(tm => tm.AboutUsSectionId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
