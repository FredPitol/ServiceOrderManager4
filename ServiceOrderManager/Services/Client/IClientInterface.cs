using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Services.Client
{
    public interface IClientInterface 
    {
        Task<ClientModel> CreateClient(DtoClientCreator dtoClientCreator, IFormFile photo); // Criação de metodo 

        Task<List<ClientModel>> GetClients(); // 10.1 lista Todos os clientesz
        Task<ClientModel> GetClientById(int id); // 10.1 Retorna cliente especifico via ID
        Task<ClientModel> EditClient(ClientModel client, IFormFile? photo); 
    }
}
