using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
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
        public async Task AddOrUpdateAboutUsAsync(AboutUsRequestDto viewModel)
        {
            var aboutUs = viewModel.Id.HasValue
                ? await _context.AboutUs
                      .Include(a => a.TeamMembers)
                      .FirstOrDefaultAsync(a => a.Id == viewModel.Id)
                : new AboutUs { Id = Guid.NewGuid(), TeamMembers = new List<TeamMember>() };

            if (aboutUs == null)
                throw new Exception("AboutUs not found.");

            aboutUs.Title = viewModel.Title;
            aboutUs.Mission = viewModel.Mission;
            aboutUs.Story = viewModel.Story;

            // Clear existing team members and re-add (simplest way)
            if (viewModel.Id.HasValue)
            {
                _context.TeamMembers.RemoveRange(aboutUs.TeamMembers);
            }

            aboutUs.TeamMembers = viewModel.TeamMembers.Select(tm => new TeamMember
            {
                Id = tm.Id ?? Guid.NewGuid(),
                Name = tm.Name,
                Role = tm.Role,
                PhotoUrl = tm.PhotoUrl,
                Bio = tm.Bio,
                AboutUsSectionId = aboutUs.Id
            }).ToList();

            if (!viewModel.Id.HasValue)
                _context.AboutUs.Add(aboutUs);

            await _context.SaveChangesAsync();
        }

        public async Task<AboutUsResponseDto> GetAboutUsForEditAsync()
        {
            var aboutUs = await _context.AboutUs
                .Include(a => a.TeamMembers)
                .FirstOrDefaultAsync();

            if (aboutUs == null)
                return new AboutUsResponseDto(); 

            return new AboutUsResponseDto
            {
                Id = aboutUs.Id,
                Title = aboutUs.Title,
                Mission = aboutUs.Mission,
                Story = aboutUs.Story,
                TeamMembers = aboutUs.TeamMembers.Select(tm => new TeamMemberResponseDto
                {
                    Id = tm.Id,
                    Name = tm.Name,
                    Role = tm.Role,
                    PhotoUrl = tm.PhotoUrl,
                    Bio = tm.Bio
                }).ToList()
            };
        }


    }
}
