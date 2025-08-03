using Microsoft.AspNetCore.Mvc;
using UserRoles.Models.Trek;
using UserRoles.Services.Interface;
using UserRoles.ViewModels;

namespace UserRoles.Controllers
{
    public class TrekContentController : Controller
    {
        private readonly ITrekContentService _trekPackageService;

        public TrekContentController(ITrekContentService trekPackageService)
        {
            _trekPackageService = trekPackageService;
        }

        public async Task<IActionResult> Index()
        {
            var trekPackages = await _trekPackageService.GetAllTrekPackagesAsync();
            return View(trekPackages);
        }

        [Route("trip/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var viewModel = await _trekPackageService.GetTrekPackageDisplayViewModelAsync(slug);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View("TrekDetails", viewModel);
        }

        [Route("featured-treks")]
        public async Task<IActionResult> Featured()
        {
            var featuredTreks = await _trekPackageService.GetFeaturedTrekPackagesAsync();
            return View(featuredTreks);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string q, string difficulty = null, string region = null)
        {
            var results = await _trekPackageService.SearchTrekPackagesAsync(q, difficulty, region);
            ViewBag.SearchTerm = q;
            ViewBag.Difficulty = difficulty;
            ViewBag.Region = region;
            return View(results);
        }

        // Admin Routes
        [Route("admin/treks")]
        public async Task<IActionResult> AdminIndex()
        {
            var trekPackages = await _trekPackageService.GetAllTrekPackagesAsync();
            return View("Admin/Index", trekPackages);
        }

      
        public IActionResult Create()
        {
            var viewModel = new TrekPackageCreateViewModel();

            // Initialize with default values
            viewModel.Currency = "USD";
            viewModel.GroupPricing.Add(new GroupPricingViewModel { GroupSize = "1 Pax", PricePerPerson = 0 });
            viewModel.GroupPricing.Add(new GroupPricingViewModel { GroupSize = "2-3 Pax", PricePerPerson = 0 });
            viewModel.GroupPricing.Add(new GroupPricingViewModel { GroupSize = "4-9 Pax", PricePerPerson = 0 });
            viewModel.GroupPricing.Add(new GroupPricingViewModel { GroupSize = "10-14 Pax", PricePerPerson = 0 });

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrekPackageCreateViewModel viewModel)
        {
          
                try
                {
                    // Check if slug already exists
                    if (await _trekPackageService.SlugExistsAsync(viewModel.Slug))
                    {
                        ModelState.AddModelError("Slug", "This slug already exists. Please use a different one.");
                        return View("Create", viewModel);
                    }

                    var trekPackage = MapViewModelToEntity(viewModel);
                    await _trekPackageService.CreateTrekPackageAsync(trekPackage);

                    TempData["Success"] = "Trek package created successfully!";
                    return RedirectToAction("AdminIndex");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating trek package: {ex.Message}");
                }
            
            return View("Create", viewModel);
        }

        [Route("admin/treks/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var trekPackage = await _trekPackageService.GetTrekPackageByIdAsync(id);
            if (trekPackage == null)
            {
                return NotFound();
            }

            var viewModel = MapEntityToViewModel(trekPackage);
            return View("Admin/Edit", viewModel);
        }

        [HttpPost]
        [Route("admin/treks/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrekPackageCreateViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if slug already exists (excluding current record)
                    if (await _trekPackageService.SlugExistsAsync(viewModel.Slug, id))
                    {
                        ModelState.AddModelError("Slug", "This slug already exists. Please use a different one.");
                        return View("Admin/Edit", viewModel);
                    }

                    var trekPackage = MapViewModelToEntity(viewModel);
                    await _trekPackageService.UpdateTrekPackageAsync(trekPackage);

                    TempData["Success"] = "Trek package updated successfully!";
                    return RedirectToAction("AdminIndex");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating trek package: {ex.Message}");
                }
            }
            return View("Admin/Edit", viewModel);
        }

        [Route("admin/treks/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trekPackage = await _trekPackageService.GetTrekPackageByIdAsync(id);
            if (trekPackage == null)
            {
                return NotFound();
            }
            return View("Admin/Delete", trekPackage);
        }

        [HttpPost, ActionName("Delete")]
        [Route("admin/treks/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _trekPackageService.DeleteTrekPackageAsync(id);
                TempData["Success"] = "Trek package deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting trek package: {ex.Message}";
            }
            return RedirectToAction("AdminIndex");
        }

        // API Endpoints for AJAX
        [HttpPost]
        public async Task<IActionResult> CheckSlugAvailability(string slug, int? excludeId = null)
        {
            var exists = await _trekPackageService.SlugExistsAsync(slug, excludeId);
            return Json(new { available = !exists });
        }

        [HttpPost]
        public IActionResult GenerateSlug(string title)
        {
            var slug = title?.ToLower()
                .Replace(" ", "-")
                .Replace("&", "and")
                .Trim('-');

            // Remove special characters
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");

            return Json(new { slug });
        }

        // Booking and Inquiry endpoints
        [HttpPost]
        public async Task<IActionResult> SubmitBooking([FromBody] BookingRequest request)
        {
            try
            {
                // Handle booking submission logic here
                // This could involve saving to database, sending emails, etc.

                return Json(new { success = true, message = "Booking request submitted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error submitting booking request." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitInquiry([FromBody] InquiryRequest request)
        {
            try
            {
                // Handle inquiry submission logic here

                return Json(new { success = true, message = "Inquiry submitted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error submitting inquiry." });
            }
        }

        private TrekPackage MapViewModelToEntity(TrekPackageCreateViewModel viewModel)
        {
            var trekPackage = new TrekPackage
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Slug = viewModel.Slug,
                ShortDescription = viewModel.ShortDescription,
                Duration = viewModel.Duration,
                Difficulty = viewModel.Difficulty,
                MaxAltitude = viewModel.MaxAltitude,
                TrekkingDistance = viewModel.TrekkingDistance,
                StartEndPoint = viewModel.StartEndPoint,
                BestSeason = viewModel.BestSeason,
                FeaturedImageUrl = viewModel.FeaturedImageUrl,
                MapImageUrl = viewModel.MapImageUrl,
                VideoUrl = viewModel.VideoUrl,
                IsFeatured = viewModel.IsFeatured
            };

            // Overview
            if (!string.IsNullOrWhiteSpace(viewModel.OverviewDescription) ||
                viewModel.OverviewKeyPoints?.Any(x => !string.IsNullOrWhiteSpace(x)) == true)
            {
                trekPackage.Overview = new TrekOverview
                {
                    Description = viewModel.OverviewDescription,
                    ShortItinerary = viewModel.ShortItinerary,
                    ImportantNote = viewModel.ImportantNote,
                    KeyPoints = viewModel.OverviewKeyPoints?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>()
                };
            }

            // Itinerary
            if (viewModel.ItineraryDays?.Any(d => !string.IsNullOrWhiteSpace(d.Title)) == true)
            {
                trekPackage.Itinerary = new TrekItinerary
                {
                    Days = viewModel.ItineraryDays
                        .Where(day => !string.IsNullOrWhiteSpace(day.Title))
                        .Select(day => new TrekItineraryDay
                        {
                            DayNumber = day.DayNumber,
                            Title = day.Title,
                            Description = day.Description,
                            Altitude = day.Altitude,
                            Duration = day.Duration,
                            Accommodation = day.Accommodation,
                            Meals = day.Meals,
                            Activities = day.Activities,
                            Transportation = day.Transportation
                        }).ToList()
                };
            }

            // Cost Info
            trekPackage.CostInfo = new TrekCostInfo
            {
                BasePrice = viewModel.BasePrice,
                Currency = viewModel.Currency,
                PriceNote = viewModel.PriceNote,
                GroupPricing = viewModel.GroupPricing?
                    .Where(gp => !string.IsNullOrWhiteSpace(gp.GroupSize))
                    .Select(gp => new TrekPricing
                    {
                        GroupSize = gp.GroupSize,
                        PricePerPerson = gp.PricePerPerson
                    }).ToList() ?? new List<TrekPricing>(),
                Breakdown = viewModel.PriceBreakdown?
                    .Where(pb => !string.IsNullOrWhiteSpace(pb.Item))
                    .Select(pb => new TrekPriceBreakdown
                    {
                        Category = pb.Category,
                        Item = pb.Item,
                        Description = pb.Description,
                        IsIncluded = pb.IsIncluded
                    }).ToList() ?? new List<TrekPriceBreakdown>()
            };

            // Other collections
            if (viewModel.FAQs?.Any(faq => !string.IsNullOrWhiteSpace(faq.Question)) == true)
            {
                trekPackage.FAQs = viewModel.FAQs
                    .Where(faq => !string.IsNullOrWhiteSpace(faq.Question))
                    .Select(faq => new TrekFAQ
                    {
                        Category = faq.Category,
                        Question = faq.Question,
                        Answer = faq.Answer,
                        SortOrder = faq.SortOrder
                    }).ToList();
            }

            if (viewModel.GalleryImages?.Any(img => !string.IsNullOrWhiteSpace(img.ImageUrl)) == true)
            {
                trekPackage.GalleryImages = viewModel.GalleryImages
                    .Where(img => !string.IsNullOrWhiteSpace(img.ImageUrl))
                    .Select(img => new TrekGalleryImage
                    {
                        ImageUrl = img.ImageUrl,
                        AltText = img.AltText,
                        Caption = img.Caption,
                        IsFeatured = img.IsFeatured,
                        SortOrder = img.SortOrder
                    }).ToList();
            }

            if (viewModel.Includes?.Any(inc => !string.IsNullOrWhiteSpace(inc.Item)) == true)
            {
                trekPackage.Includes = viewModel.Includes
                    .Where(inc => !string.IsNullOrWhiteSpace(inc.Item))
                    .Select(inc => new TrekInclusion
                    {
                        Category = inc.Category,
                        Item = inc.Item,
                        Description = inc.Description,
                        IsIncluded = true,
                        SortOrder = inc.SortOrder
                    }).ToList();
            }

            if (viewModel.Excludes?.Any(exc => !string.IsNullOrWhiteSpace(exc.Item)) == true)
            {
                trekPackage.Excludes = viewModel.Excludes
                    .Where(exc => !string.IsNullOrWhiteSpace(exc.Item))
                    .Select(exc => new TrekInclusion
                    {
                        Category = exc.Category,
                        Item = exc.Item,
                        Description = exc.Description,
                        IsIncluded = false,
                        SortOrder = exc.SortOrder
                    }).ToList();
            }

            if (viewModel.Highlights?.Any(h => !string.IsNullOrWhiteSpace(h.Title)) == true)
            {
                trekPackage.Highlights = viewModel.Highlights
                    .Where(h => !string.IsNullOrWhiteSpace(h.Title))
                    .Select(h => new TrekHighlight
                    {
                        Title = h.Title,
                        Description = h.Description,
                        IconClass = h.IconClass,
                        SortOrder = h.SortOrder
                    }).ToList();
            }

            if (viewModel.Departures?.Any(d => d.StartDate != default) == true)
            {
                trekPackage.Departures = viewModel.Departures
                    .Where(d => d.StartDate != default)
                    .Select(d => new TrekDeparture
                    {
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        AvailableSpots = d.AvailableSpots,
                        Price = d.Price,
                        Status = d.Status,
                        Notes = d.Notes
                    }).ToList();
            }

            return trekPackage;
        }

        private TrekPackageCreateViewModel MapEntityToViewModel(TrekPackage trekPackage)
        {
            var viewModel = new TrekPackageCreateViewModel
            {
                Id = trekPackage.Id,
                Title = trekPackage.Title,
                Slug = trekPackage.Slug,
                ShortDescription = trekPackage.ShortDescription,
                Duration = trekPackage.Duration,
                Difficulty = trekPackage.Difficulty,
                MaxAltitude = trekPackage.MaxAltitude,
                TrekkingDistance = trekPackage.TrekkingDistance,
                StartEndPoint = trekPackage.StartEndPoint,
                BestSeason = trekPackage.BestSeason,
                FeaturedImageUrl = trekPackage.FeaturedImageUrl,
                MapImageUrl = trekPackage.MapImageUrl,
                VideoUrl = trekPackage.VideoUrl,
                IsFeatured = trekPackage.IsFeatured,
                OverviewDescription = trekPackage.Overview?.Description,
                ShortItinerary = trekPackage.Overview?.ShortItinerary,
                ImportantNote = trekPackage.Overview?.ImportantNote,
                OverviewKeyPoints = trekPackage.Overview?.KeyPoints ?? new List<string>(),
                BasePrice = trekPackage.CostInfo?.BasePrice ?? 0,
                Currency = trekPackage.CostInfo?.Currency ?? "USD",
                PriceNote = trekPackage.CostInfo?.PriceNote
            };

            // Map collections
            if (trekPackage.CostInfo?.GroupPricing != null)
            {
                viewModel.GroupPricing = trekPackage.CostInfo.GroupPricing
                    .Select(gp => new GroupPricingViewModel
                    {
                        GroupSize = gp.GroupSize,
                        PricePerPerson = gp.PricePerPerson
                    }).ToList();
            }

            // Map other collections similarly...

            return viewModel;
        }
    }

    // Request models for API endpoints
    public class BookingRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public int NumberOfTravelers { get; set; }
        public DateTime? PreferredStartDate { get; set; }
        public string SpecialRequirements { get; set; }
        public int TrekPackageId { get; set; }
    }

    public class InquiryRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int TrekPackageId { get; set; }
    }
}

