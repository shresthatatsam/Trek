using UserRoles.Data;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Services
{
    public class CarousalServices :ICarousalService
    {
        private readonly AppDbContext _context;

        public CarousalServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarousalImageResponseDto> Create(CarousalImageRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            string imageUrl = null;

            // If there's an image file, save it to disk (like you did before)
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "carousel-images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{dto.ImageFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }

                imageUrl = $"/carousel-images/{uniqueFileName}";
            }

            var carouselImage = new CarousalImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = imageUrl,
                Caption = dto.Caption,
                SubCaption = dto.SubCaption,
                IsActive = dto.IsActive,
                carousalEnum = dto.carousalEnum 
            };

            _context.CarousalImages.Add(carouselImage);
            await _context.SaveChangesAsync();

            // Prepare and return response DTO
            var responseDto = new CarousalImageResponseDto
            {
                Id = carouselImage.Id,
                ImageUrl = carouselImage.ImageUrl,
                Caption = carouselImage.Caption,
                SubCaption = carouselImage.SubCaption,
                IsActive = carouselImage.IsActive,
                carousalEnum = carouselImage.carousalEnum
            };

            return responseDto;
        }


    }
}
