using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;

namespace UserRoles.Services.Interface
{
    public interface ICarousalService
    {
        Task<CarousalImageResponseDto> Create(CarousalImageRequestDto dto);
    }
}
