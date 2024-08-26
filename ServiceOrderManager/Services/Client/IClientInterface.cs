using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Services.Client


{
    public interface IClientInterface
    {
        Task<ClientModel> CreateClient(DtoClientCreator dtoClientCreated, IFormFile photo);

    }
}
