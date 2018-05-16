using Microsoft.AspNetCore.Mvc;

namespace London.ViewComponents
{
    public class NavigationBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
