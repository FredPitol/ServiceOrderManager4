using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Services.Client;

namespace ServiceOrderManager.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientInterface _clientInterface;

        //9.5 <- Injeção de dependencia 
        public ClientController(IClientInterface clientInterface)
        {
            _clientInterface = clientInterface;
        }

        public async Task<IActionResult> Index()
        {
            //10.3 index Recebe lista de clientes
            var clients = await _clientInterface.GetClients();
            return View(clients); //Retorna lista de clientes
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
                // 9.4 <- Precisamos entrar na interface ->
                // 9.6 Passamos o dto e a foto 
                var client = await _clientInterface.CreateClient(dtoClientCreator, photo);// Método criado na interface <-
                return RedirectToAction("Index", "Client"); //Redireciona para action Index no controller client 
            }
            else // Devolvemos a view preenchida
            {
                return View(dtoClientCreator);
            }
        }
    }
}
