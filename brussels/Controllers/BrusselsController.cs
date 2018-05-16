using Microsoft.AspNetCore.Mvc;

namespace Brussels.Controllers
{
    public class BrusselsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
