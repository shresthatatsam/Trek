using Microsoft.AspNetCore.Mvc;

namespace UserRoles.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
