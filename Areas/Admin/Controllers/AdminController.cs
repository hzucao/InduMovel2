using System.Numerics;
using Microsoft.AspNetCore.Mvc;
namespace InduMovel.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}