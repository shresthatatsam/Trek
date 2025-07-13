using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Models;

namespace UserRoles.Services.Interface
{
    public interface IAboutUsService
    {
        Task AddOrUpdateAboutUsAsync(AboutUsRequestDto viewModel);
        //Task<AboutUsResponseDto> GetAboutUsForEditAsync();
    }
}
  