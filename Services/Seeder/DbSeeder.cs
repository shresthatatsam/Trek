using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models;

namespace UserRoles.Services.Seeder
{
    public class DbSeeder
    {
        public static void SeedAboutUs(AppDbContext context)
        {
            if (!context.AboutUs.Any())
            {
                context.AboutUs.Add(new AboutUs
                {
                    Title = "Travel made easy",
                    Story = "something",
                    Mission = "abc",
                    IsActive = true,
                    ImageUrl = "https://media.istockphoto.com/photos/about-us-picture-id816887384?k=20&m=816887384&s=612x612&w=0&h=P3p2ciEwnLwDAK-7JoWZDTZJl9uRrJaGdLhuVbmMkJg="
                });

                context.SaveChanges();
            }
        }
    }
}
