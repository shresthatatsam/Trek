using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly AppDbContext _context;

        public AboutUsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AboutUs> GetAboutUsAsync()
        {
             return await _context.AboutUs
                    .Include(a => a.TeamMembers)
                    .FirstOrDefaultAsync();
        }

        public async Task UpdateAboutUsAsync(AboutUs aboutUs)
        {
            var existing = await _context.AboutUs.Include(a => a.TeamMembers).FirstOrDefaultAsync();
            if (existing != null)
            {
                existing.Title = aboutUs.Title;
                existing.Mission = aboutUs.Mission;
                existing.Story = aboutUs.Story;
                _context.Update(existing);
            }
            else
            {
                _context.Add(aboutUs);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddTeamMemberAsync(TeamMember member)
        {
            _context.TeamMembers.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeamMemberAsync(Guid memberId)
        {
            var member = await _context.TeamMembers.FindAsync(memberId);
            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}
