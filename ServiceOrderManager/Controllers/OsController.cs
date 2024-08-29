using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Services.Client;

using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using ServiceOrderManager.Services.Os;

namespace ServiceOrderManager.Controllers
{
    public class OsController : Controller
    {
        private readonly IOsInterface _osInterface;

        public OsController(IOsInterface osInterface)
        {
            _osInterface = osInterface;
        }

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

