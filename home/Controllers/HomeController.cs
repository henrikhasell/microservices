using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
