using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Dto;

namespace ServiceOrderManager.Controllers
{

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

        // 9. Criando método post
        [HttpPost]
        public async Task<IActionResult> Enroll(DtoClientCreator dtoClientCreator, IFormFile photo)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                return View(dtoClientCreator);
            }
        }
    }
}
