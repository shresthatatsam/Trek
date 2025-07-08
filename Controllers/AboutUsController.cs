using Microsoft.AspNetCore.Mvc;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarousalService _carousalService;
        private readonly IAboutUsService _service;
        public AboutUsController(ILogger<HomeController> logger, ICarousalService carousalService, IAboutUsService service)
        {
            _logger = logger;
            _carousalService = carousalService;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var carouselImages = await _carousalService.List(CarousalEnum.Content);
            //var aboutUs = await _service.GetAboutUsAsync();
            //ViewBag.CarouselImages = carouselImages;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var aboutUs = await _service.GetAboutUsAsync();
            return View(aboutUs);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutUs model)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAboutUsAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamMember(TeamMember member)
        {
            if (ModelState.IsValid)
            {
                await _service.AddTeamMemberAsync(member);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeamMember(Guid id)
        {
            await _service.RemoveTeamMemberAsync(id);
            return RedirectToAction("Index");
        }

    }
}
