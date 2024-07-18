using Microsoft.AspNetCore.Mvc;
using Scrap_Project.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Scrap_Project.Controllers
{
    public class MainController : Controller
    {
        string constring = Configuration.GetConnectionString("DefaultConnection");

        SqlConnection con = new SqlConnection()
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
