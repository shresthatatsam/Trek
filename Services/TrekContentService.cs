using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models.Trek;
using UserRoles.Services.Interface;
using UserRoles.ViewModels;

namespace UserRoles.Services
{
    public class TrekContentService :  ITrekContentService
    {
        private readonly AppDbContext _context;

        public TrekContentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TrekPackage> CreateTrekPackageAsync(TrekPackage trekPackage)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Set timestamps
                trekPackage.CreatedAt = DateTime.UtcNow;
                trekPackage.UpdatedAt = DateTime.UtcNow;

                // Clear foreign key references
                ClearForeignKeyReferences(trekPackage);

                // Add and save the main entity first
                _context.TrekPackages.Add(trekPackage);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return trekPackage;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error creating trek package: {ex.Message}", ex);
            }
        }

        public async Task<TrekPackage> GetTrekPackageByIdAsync(int id)
        {
            return await _context.TrekPackages
                .Include(t => t.Overview)
                .Include(t => t.Itinerary)
                    .ThenInclude(i => i.Days.OrderBy(d => d.DayNumber))
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.GroupPricing)
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.Breakdown)
                .Include(t => t.FAQs.OrderBy(f => f.SortOrder))
                .Include(t => t.GalleryImages.OrderBy(g => g.SortOrder))
                .Include(t => t.Includes.Where(i => i.IsIncluded).OrderBy(i => i.SortOrder))
                .Include(t => t.Excludes.Where(e => !e.IsIncluded).OrderBy(e => e.SortOrder))
                .Include(t => t.Highlights.OrderBy(h => h.SortOrder))
                .Include(t => t.Reviews.Where(r => r.IsVerified).OrderByDescending(r => r.ReviewDate))
                .Include(t => t.Departures.Where(d => d.StartDate >= DateTime.Today).OrderBy(d => d.StartDate))
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TrekPackage> GetTrekPackageBySlugAsync(string slug)
        {
            return await _context.TrekPackages
                .Include(t => t.Overview)
                .Include(t => t.Itinerary)
                    .ThenInclude(i => i.Days.OrderBy(d => d.DayNumber))
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.GroupPricing)
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.Breakdown)
                .Include(t => t.FAQs.OrderBy(f => f.SortOrder))
                .Include(t => t.GalleryImages.OrderBy(g => g.SortOrder))
                .Include(t => t.Includes.Where(i => i.IsIncluded).OrderBy(i => i.SortOrder))
                .Include(t => t.Excludes.Where(e => !e.IsIncluded).OrderBy(e => e.SortOrder))
                .Include(t => t.Highlights.OrderBy(h => h.SortOrder))
                .Include(t => t.Reviews.Where(r => r.IsVerified).OrderByDescending(r => r.ReviewDate))
                .Include(t => t.Departures.Where(d => d.StartDate >= DateTime.Today).OrderBy(d => d.StartDate))
                .FirstOrDefaultAsync(t => t.Slug == slug);
        }

        public async Task<TrekPackageDisplayViewModel> GetTrekPackageDisplayViewModelAsync(string slug)
        {
            var trekPackage = await GetTrekPackageBySlugAsync(slug);
            if (trekPackage == null) return null;

            var viewModel = new TrekPackageDisplayViewModel
            {
                TrekPackage = trekPackage,
                FAQsByCategory = trekPackage.FAQs
                    .GroupBy(f => f.Category ?? "General")
                    .ToDictionary(g => g.Key, g => g.OrderBy(f => f.SortOrder).ToList()),
                IncludesByCategory = trekPackage.Includes
                    .GroupBy(i => i.Category ?? "General")
                    .ToDictionary(g => g.Key, g => g.OrderBy(i => i.SortOrder).ToList()),
                ExcludesByCategory = trekPackage.Excludes
                    .GroupBy(e => e.Category ?? "General")
                    .ToDictionary(g => g.Key, g => g.OrderBy(e => e.SortOrder).ToList()),
                LatestReviews = trekPackage.Reviews.Take(5).ToList(),
                AverageRating = trekPackage.Reviews.Any() ? trekPackage.Reviews.Average(r => r.Rating) : 0,
                TotalReviews = trekPackage.Reviews.Count,
                AvailableDepartures = trekPackage.Departures
                    .Where(d => d.StartDate >= DateTime.Today)
                    .OrderBy(d => d.StartDate)
                    .Take(10)
                    .ToList()
            };

            return viewModel;
        }

        public async Task<IEnumerable<TrekPackage>> GetAllTrekPackagesAsync()
        {
            return await _context.TrekPackages
                .Include(t => t.Overview)
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.GroupPricing)
                .OrderByDescending(t => t.IsFeatured)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrekPackage>> GetFeaturedTrekPackagesAsync()
        {
            return await _context.TrekPackages
                .Where(t => t.IsFeatured)
                .Include(t => t.Overview)
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.GroupPricing)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TrekPackage> UpdateTrekPackageAsync(TrekPackage trekPackage)
        {
            try
            {
                trekPackage.UpdatedAt = DateTime.UtcNow;
                _context.Entry(trekPackage).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return trekPackage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating trek package: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteTrekPackageAsync(int id)
        {
            try
            {
                var trekPackage = await _context.TrekPackages.FindAsync(id);
                if (trekPackage == null)
                    return false;

                _context.TrekPackages.Remove(trekPackage);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting trek package: {ex.Message}", ex);
            }
        }

        public async Task<bool> TrekPackageExistsAsync(int id)
        {
            return await _context.TrekPackages.AnyAsync(t => t.Id == id);
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            var query = _context.TrekPackages.Where(t => t.Slug == slug);
            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<IEnumerable<TrekPackage>> SearchTrekPackagesAsync(string searchTerm, string difficulty = null, string region = null)
        {
            var query = _context.TrekPackages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm) ||
                                        t.ShortDescription.Contains(searchTerm) ||
                                        t.Overview.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(difficulty))
            {
                query = query.Where(t => t.Difficulty == difficulty);
            }

            return await query
                .Include(t => t.Overview)
                .Include(t => t.CostInfo)
                    .ThenInclude(c => c.GroupPricing)
                .OrderByDescending(t => t.IsFeatured)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        private void ClearForeignKeyReferences(TrekPackage trekPackage)
        {
            if (trekPackage.Overview != null)
                trekPackage.Overview.TrekPackageId = 0;

            if (trekPackage.Itinerary != null)
            {
                trekPackage.Itinerary.TrekPackageId = 0;
                foreach (var day in trekPackage.Itinerary.Days ?? new List<TrekItineraryDay>())
                    day.TrekItineraryId = 0;
            }

            if (trekPackage.CostInfo != null)
            {
                trekPackage.CostInfo.TrekPackageId = 0;
                foreach (var pricing in trekPackage.CostInfo.GroupPricing ?? new List<TrekPricing>())
                    pricing.TrekCostInfoId = 0;
                foreach (var breakdown in trekPackage.CostInfo.Breakdown ?? new List<TrekPriceBreakdown>())
                    breakdown.TrekCostInfoId = 0;
            }

            foreach (var faq in trekPackage.FAQs ?? new List<TrekFAQ>())
                faq.TrekPackageId = 0;
            foreach (var image in trekPackage.GalleryImages ?? new List<TrekGalleryImage>())
                image.TrekPackageId = 0;
            foreach (var include in trekPackage.Includes ?? new List<TrekInclusion>())
                include.TrekPackageId = 0;
            foreach (var exclude in trekPackage.Excludes ?? new List<TrekInclusion>())
                exclude.TrekPackageId = 0;
            foreach (var highlight in trekPackage.Highlights ?? new List<TrekHighlight>())
                highlight.TrekPackageId = 0;
            foreach (var departure in trekPackage.Departures ?? new List<TrekDeparture>())
                departure.TrekPackageId = 0;
        }
    }
}
