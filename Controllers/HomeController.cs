using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Models;
using UserRoles.Services.Interface;

namespace UserRoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarousalService _carousalService;
        public HomeController(ILogger<HomeController> logger, ICarousalService carousalService)
        {
            _logger = logger;
            _carousalService = carousalService;
        }



     

        public async Task<IActionResult> Index()
        {   
            var carouselImages = await _carousalService.List(CarousalEnum.Home);
            ViewBag.CarouselImages = carouselImages;
            return View();
        }

      


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
