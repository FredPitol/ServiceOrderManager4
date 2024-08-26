using ServiceOrderManager.Data;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
// Imports ok 

namespace ServiceOrderManager.Services.Client
{
    public class ClientService : IClientInterface
    {
        // 9. Implementação do método

        private readonly AppDbContext _context;
        private readonly string _system;

        // 9. Construtor - Injeção de dependencia

        public ClientService(AppDbContext context, IWebHostEnvironment system)
        {
            // 9. Acesso ao banco 
            _context = context;
            _system = system.WebRootPath;
       
        }

        // Gera caminho 

        public string CreateFilePath(IFormFile photo)
        {
            var uniqueId = Guid.NewGuid().ToString();   // Cria idunico  
      
            var imagePathName = photo.FileName.Replace(" ", "").ToLower() + uniqueId + ".png";  // Monta image path
                                                                                                // 
            var pathToSavePhoto = _system + "\\imagem\\";    // Cria path para armazenar foto no wwroot (_system)

            // Cria diretório caso não exista 
            if (!Directory.Exists(pathToSavePhoto)) // Se existe troca pra f para não entrar 
            {
                Directory.CreateDirectory(pathToSavePhoto);
            }

            // 9.1 Criando foto dentro do caminho de imagem 
            using (var stream = File.Create(pathToSavePhoto + imagePathName))
            {
                //9. Cria copia da foto nesse caminho de imagem
                photo.CopyToAsync(stream).Wait(); 
            }

            return imagePathName;

        }
        //9.2 Temos a pasta criada e o nome da imagem que foi criada que vamos adicionar na propriedade a seguir 
        //9. Criado implementando a interface 
        public async Task<ClientModel> CreateClient(DtoClientCreator dtoClientCreator, IFormFile photo)
        {
            try
            {
                var imagePathName = CreateFilePath(photo); 

                var client = new ClientModel
                {
                    Name = dtoClientCreator.Name,
                    Email = dtoClientCreator.Email,
                    Address = dtoClientCreator.Address,
                    Cnpj = dtoClientCreator.Cnpj,
                    Logo = imagePathName
                };

                // 9.3 Atualizando bd com o objeto criado 
                _context.Add(client);
                await _context.SaveChangesAsync(); // 9.4 Espera que o processo de salvar no banco seja feito , agora podemos ir para ClientControoller -> 

                return client;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // 10.2 Listando todos clientes
        public async Task<List<ClientModel>> GetClients()
        {
            try
            {   //10. Entra no banco e cria lista, possibilitando que o controller index acesse 
                return await _context.Clients.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Task<ClientModel> GetClientById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
