using Microsoft.AspNetCore.Mvc;

namespace Paris.Controllers
{
    public class ParisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
