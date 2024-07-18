using Microsoft.AspNetCore.Mvc;

namespace Scrap_Project.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
