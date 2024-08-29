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

        public async Task<IActionResult> Index(string? search)
        {
            if (search == null)
            {
                var clients = await _osInterface.GetOrders();
                return View(clients);
            }
            else
            {
                //14.3 Filtro aplicado, adicionar a interface -> 
                var clients = await _osInterface.GetOrdersFilter(search);
                return View(clients);

            }
        }

        public IActionResult Enroll()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _osInterface.GetOsById(id);

            return View(client);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = await _osInterface.RemoveOs(id);
            return RedirectToAction("Index", "Os");
        }

        [HttpPost]

        public async Task<IActionResult> Enroll(DtoOsCreator dtoOsCreator, IFormFile photo)
        {
            if (ModelState.IsValid) // Info validas ?
            {
                var client = await _osInterface.CreateOs(dtoOsCreator, photo); //Passar objeto?
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return View(dtoOsCreator);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(OsModel osModel, IFormFile? photo)
        {
            if (ModelState.IsValid)
            {
                var client = await _osInterface.EditOs(osModel, photo);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return View(osModel);
            }

        }

    }

}

