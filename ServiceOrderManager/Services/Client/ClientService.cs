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
        public string CreateImagePath(IFormFile photo)
        {
            var uniqueId = Guid.NewGuid().ToString();
            var ImagePathName = photo.FileName.Replace(" ", "").ToLower() + uniqueId + ".png";
            var pathToSavePhoto = _system + "\\image\\";

            // Cria diretório caso não exista 
            if (!Directory.Exists(pathToSavePhoto))
            {
                Directory.CreateDirectory(pathToSavePhoto);
            }

            using (var stream = File.Create(pathToSavePhoto + ImagePathName))
            {
                //9. Cria copia da foto nesse caminho de imagem
                photo.CopyToAsync(stream).Wait();
            }

            return pathToSavePhoto;

        }
        //9. Criado implementando a interface (auto)
        public async Task<ClientModel> CreateClient(DtoClientCreator dtoClientCreated, IFormFile photo)
        {
            try
            {
                var nameImagePath = CreateImagePath(photo);

                var client = new ClientModel
                {
                    Name = dtoClientCreated.Name,
                    Email = dtoClientCreated.Email,
                    Address = dtoClientCreated.Address,
                    Cnpj = dtoClientCreated.Cnpj,
                    Logo = nameImagePath
                };
                // Atualizando bd
                _context.Add(client);
                await _context.SaveChangesAsync();

                return client;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
