using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
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


        public async Task<IActionResult> Create()
        {  
            var model = new AboutUsRequestDto();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsRequestDto model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddOrUpdateAboutUsAsync(model);
                return RedirectToAction("Create");
            }
            return View(model);
        }

        //public async Task<IActionResult> Update(Guid id)
        //{
        //    var aboutUs = await _service.GetByIdAsync(id);
        //    if (aboutUs == null) return NotFound();
        //    return View(aboutUs);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AboutUsRequestDto model)
        {
            if (ModelState.IsValid)
            {
                //await _service.AddOrUpdateAboutUsAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }



    }
}
