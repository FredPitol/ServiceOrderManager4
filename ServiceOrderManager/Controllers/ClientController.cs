using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using ServiceOrderManager.Services.Client;

namespace ServiceOrderManager.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientInterface _clientInterface;
    
        public ClientController(IClientInterface clientInterface)
        {
            _clientInterface = clientInterface;
        }

        public async Task<IActionResult> Index(string? search)
        {
            //10.3
           
            //14.2 
            
            if(search == null)
            {
                var clients = await _clientInterface.GetClients();
                return View(clients);
            }
            else
            {
                //14.3 Filtro aplicado, adicionar a interface -> 
                var clients = await _clientInterface.GetClientsFilter(search);
                return View(clients);

            }
        }

 
        public IActionResult Enroll()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientInterface.GetClientById(id);

            return View(client);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = await _clientInterface.RemoveClient(id);
            return RedirectToAction("Index", "Client");
        }
 
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
