using Microsoft.AspNetCore.Mvc;

namespace ServiceOrderManager.Controllers
{
    // Perdi 1hora procurando esse 1 -_-
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Enroll()
        {
            return View();
        }
    }
}
