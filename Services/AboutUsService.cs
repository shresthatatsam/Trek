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
        private readonly IFileService _fileService;
        public AboutUsService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }


        public async Task AddOrUpdateAboutUsAsync(AboutUsRequestDto viewModel)
        {
            var aboutUs = viewModel.Id.HasValue
                ? await _context.AboutUs.FirstOrDefaultAsync(a => a.Id == viewModel.Id)
                : new AboutUs { Id = Guid.NewGuid() };

            if (aboutUs == null)
                throw new Exception("AboutUs not found.");

            if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(aboutUs.ImageUrl))
                {
                    await _fileService.DeleteFileAsync(aboutUs.ImageUrl);
                }
                var imageUrl = await _fileService.SaveImageAsync(viewModel.ImageFile, "about-images");
                aboutUs.ImageUrl = imageUrl;
            }

            aboutUs.Title = viewModel.Title;
            aboutUs.Mission = viewModel.Mission;
            aboutUs.Story = viewModel.Story;

            if (!viewModel.Id.HasValue)
                _context.AboutUs.Add(aboutUs);


            await _context.SaveChangesAsync();
        }


    }
}
