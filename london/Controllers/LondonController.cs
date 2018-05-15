using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace London.Controllers
{
    public class LondonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}