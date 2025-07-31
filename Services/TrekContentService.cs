using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models.Trek;

namespace UserRoles.Services
{
    public class TrekContentService
    {
        private readonly AppDbContext _context;

        public TrekContentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TrekPackage model)
        {

            _context.TrekPackages.Add(model);
            _context.TrekPackages.Include(x => x.Excludes).Include(x=>x.Includes).Include(x=>x.FAQs);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrekCostInfo model)
        {
            //var existing = await _context.TrekCostInfos.FirstOrDefaultAsync(x => x.Id == model.Id);
            //if (existing == null)
            //    throw new Exception("TrekCostInfo not found.");

            //existing.PricePerPerson = model.PricePerPerson;
            //existing.DiscountedPrice = model.DiscountedPrice;
            //existing.CostIncludes = model.CostIncludes;
            //existing.CostExcludes = model.CostExcludes;
            //existing.AdditionalCosts = model.AdditionalCosts;
            //existing.Notes = model.Notes;
            //existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<TrekCostInfo?> GetByIdAsync(Guid id)
        {
            return await _context.TrekCostInfos.FindAsync(id);
        }
    }
}
