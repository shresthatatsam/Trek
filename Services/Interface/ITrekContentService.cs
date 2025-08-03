using UserRoles.Models.Trek;
using UserRoles.ViewModels;

namespace UserRoles.Services.Interface
{
    public interface ITrekContentService
    {
        Task<TrekPackage> CreateTrekPackageAsync(TrekPackage trekPackage);
        Task<TrekPackage> GetTrekPackageByIdAsync(int id);
        Task<TrekPackage> GetTrekPackageBySlugAsync(string slug);
        Task<TrekPackageDisplayViewModel> GetTrekPackageDisplayViewModelAsync(string slug);
        Task<IEnumerable<TrekPackage>> GetAllTrekPackagesAsync();
        Task<IEnumerable<TrekPackage>> GetFeaturedTrekPackagesAsync();
        Task<TrekPackage> UpdateTrekPackageAsync(TrekPackage trekPackage);
        Task<bool> DeleteTrekPackageAsync(int id);

        // Validation
        Task<bool> TrekPackageExistsAsync(int id);
        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);

        // Search and Filter
        Task<IEnumerable<TrekPackage>> SearchTrekPackagesAsync(string searchTerm, string difficulty = null, string region = null);

    }
}
