using ServiceOrderManager.Data;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


namespace ServiceOrderManager.Services.Client
{
    public class ClientService : IClientInterface
    {
        private readonly AppDbContext _context;

        private readonly string _system;

        public ClientService(AppDbContext context, IWebHostEnvironment system)
        {
            // 9. Acesso ao banco 
            _context = context;
            _system = system.WebRootPath;

        }

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

        public async Task<ClientModel> GetClientById(int id)
        {
            try
            {   // 11. =>
                return await _context.Clients.FirstOrDefaultAsync(client => client.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientModel> EditClient(ClientModel client, IFormFile? photo)
        {
            try
            {
                var clientFromDataBase = await _context.Clients.AsNoTracking().SingleOrDefaultAsync(clientBD => clientBD.ID == client.ID); // 11.2  

                //11.3

                var imagePathName = "";

                if (photo != null)
                {
                    string existingLogoPath = _system + "\\image\\" + clientFromDataBase.Logo;
                    if (File.Exists(existingLogoPath))
                    {
                        File.Delete(existingLogoPath);
                    }

                    imagePathName = CreateFilePath(photo);
                }

                clientFromDataBase.Address = client.Address;
                clientFromDataBase.Cnpj = client.Cnpj;
                clientFromDataBase.Email = client.Email;
                clientFromDataBase.Name = client.Name;
                //11.3 
                if (imagePathName != "")
                {
                    clientFromDataBase.Logo = imagePathName;
                }
                else
                {
                    clientFromDataBase.Logo = client.Logo;
                }

                //11.4
                _context.Update(clientFromDataBase);
                await _context.SaveChangesAsync();

                return client;

            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientModel> RemoveClient(int id)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(clientFromDataBase => clientFromDataBase.ID == id);

                _context.Remove(client);
                await _context.SaveChangesAsync();

                return client;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ClientModel>> GetClientsFilter(string? search)
        {
            try
            {
                // 14
                // Adicionar task no controller caso esteja com erro
                var clients = await _context.Clients.Where(clientFromDataBase => clientFromDataBase.Name.Contains(search)).ToListAsync();
                return clients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
