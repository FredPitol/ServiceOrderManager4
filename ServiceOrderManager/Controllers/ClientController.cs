using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Services.Client;

namespace ServiceOrderManager.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientInterface _clientInterface;

        //Construtor - Injeção de dependencia - 

        public ClientController(IClientInterface clientInterface)
        {
            _clientInterface = clientInterface;
        }
            
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Enroll()
        {
            return View();
        }


        // 9. Criando método post
        
        [HttpPost]

        public async Task<IActionResult> Enroll(DtoClientCreator dtoClientCreator, IFormFile photo)
        {
            if (ModelState.IsValid) // Info validas ?
            {   
                // Precisamos criar os métodos na interface - Controllers ->  Interface (Métodos mapeados dentro do service), possibilita o acesso 
                var client = await _clientInterface.CreateClient(dtoClientCreator, photo);// Método criado na interface <-
                return RedirectToAction("Index", "Client");
            }
            else // Devolvemos a view preenchida
            {
                return View(dtoClientCreator);
            }
        }
    }
}
