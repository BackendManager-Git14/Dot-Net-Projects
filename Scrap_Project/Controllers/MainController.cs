using Microsoft.AspNetCore.Mvc;
using Scrap_Project.Models;

namespace Scrap_Project.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(user_data model)
        {
            return View();
        }

    }
}
