using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _env;

        public CarousalServices(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public async Task<CarousalImageResponseDto?> GetById(Guid id)
        {
            return await _context.CarousalImages
                .Where(x => x.Id == id)
                .Select(x => new CarousalImageResponseDto
                {
                    Id = x.Id,
                    Caption = x.Caption,
                    SubCaption = x.SubCaption,
                    ImageUrl = x.ImageUrl,
                    carousalEnum = x.carousalEnum
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(CarousalImageRequestDto dto, IFormFile? newImageFile = null)
        {
            var entity = await _context.CarousalImages.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null) return false;

            entity.Caption = dto.Caption;
            entity.SubCaption = dto.SubCaption;
            entity.IsActive = dto.IsActive;
            entity.carousalEnum = dto.carousalEnum;

            if (newImageFile != null && newImageFile.Length > 0)
            {
                // Delete old image file if exists
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                {
                    var oldFilePath = Path.Combine(_env.WebRootPath, entity.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new image file
                var uploadsFolder = Path.Combine(_env.WebRootPath, "carousel-images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{newImageFile.FileName}";
                var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(stream);
                }

                entity.ImageUrl = $"/carousel-images/{uniqueFileName}";
            }
            // else keep old ImageUrl if no new file uploaded

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CarousalImageResponseDto>> List(CarousalEnum carousalEnum)
        {
            return await _context.CarousalImages.Where(x=>x.carousalEnum == carousalEnum && x.IsActive).AsNoTracking().Select(x=> new CarousalImageResponseDto
            {
                Id = x.Id,
                Caption = x.Caption,
                SubCaption = x.SubCaption,
                ImageUrl= x.ImageUrl

            }).ToListAsync();
        }
    
		public async Task<bool> Delete(Guid id)
		{
			// Only select ImageUrl to avoid fetching entire entity
			var imageData = await _context.CarousalImages
				.Where(x => x.Id == id)
				.Select(x => new { x.Id, x.ImageUrl })
				.FirstOrDefaultAsync();

			if (imageData == null)
				return false;

			// Build physical file path
			if (!string.IsNullOrEmpty(imageData.ImageUrl))
			{
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageData.ImageUrl.TrimStart('/'));

				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}

			// Only create entity with Id to avoid unnecessary tracking
			var entity = new CarousalImage { Id = imageData.Id };
			_context.CarousalImages.Attach(entity);
			_context.CarousalImages.Remove(entity);

			await _context.SaveChangesAsync();

			return true;
		}


	}
}
