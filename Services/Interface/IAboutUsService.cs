using UserRoles.Models;

namespace UserRoles.Services.Interface
{
    public interface IAboutUsService
    {
        Task<AboutUs> GetAboutUsAsync();
        Task UpdateAboutUsAsync(AboutUs aboutUs);
        Task AddTeamMemberAsync(TeamMember member);
        Task RemoveTeamMemberAsync(Guid memberId);
    }
}
  