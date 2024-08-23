using Microsoft.AspNetCore.Mvc;

namespace ServiceOrderManager.Controllers
{
    public class ClientController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
