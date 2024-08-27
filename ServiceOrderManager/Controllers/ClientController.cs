using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using ServiceOrderManager.Services.Client;

namespace ServiceOrderManager.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientInterface _clientInterface;

        //9.5 
        public ClientController(IClientInterface clientInterface)
        {
            _clientInterface = clientInterface;
        }

        public async Task<IActionResult> Index()
        {
            //10.3
            var clients = await _clientInterface.GetClients();
            return View(clients); 
        }

        public IActionResult Enroll()
        {
            return View();
        }

        // 11.1 
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientInterface.GetClientById(id);

            return View(client);
        }

        // 9. Criando método post
        
        [HttpPost]

        public async Task<IActionResult> Enroll(DtoClientCreator dtoClientCreator, IFormFile photo)
        {
            if (ModelState.IsValid) // Info validas ?
            {   
                var client = await _clientInterface.CreateClient(dtoClientCreator, photo);
                return RedirectToAction("Index", "Client"); 
            }
            else
            {
                return View(dtoClientCreator);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientModel clientModel,IFormFile? photo )
        {
            if(ModelState.IsValid)
            {
                var client = await _clientInterface.EditClient(clientModel, photo);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return View(clientModel);
            }

        }
    }
}
