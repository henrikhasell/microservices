using Microsoft.AspNetCore.Mvc;

namespace Authentication.ViewComponents
{
    public class NavigationBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
