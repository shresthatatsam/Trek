using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class CarousalController : Controller
    {
        private readonly ICarousalService _carousalService;
        public CarousalController( ICarousalService carousalService)
        {
            this._carousalService = carousalService;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CarousalImageRequestDto dto)
        {
            var response = await this._carousalService.Create(dto); // assuming _homeService is injected and has Create method

            if (response != null)
                return RedirectToAction("Index"); // Redirect or show success

            ModelState.AddModelError("", "Failed to upload image.");
            return View(dto);
        }

    }
}
