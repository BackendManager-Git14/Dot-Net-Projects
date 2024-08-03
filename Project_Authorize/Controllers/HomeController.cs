using Microsoft.AspNetCore.Mvc;
using Project_Authorize.Models;
using System.Diagnostics;

namespace Project_Authorize.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult register()
        {
            return View("register");
        }
        [HttpPost]
        public IActionResult register(register model)
        {
            if (!ModelState.IsValid)
            {
                return View("register");
            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
