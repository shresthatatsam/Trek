using Microsoft.AspNetCore.Mvc;

namespace UserRoles.Controllers
{
    public class TrekContentController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
