using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace InduMovel.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}