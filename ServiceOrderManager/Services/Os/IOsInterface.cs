using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Services.Os
{
    public interface IOsInterface
    {
        Task<OsModel> CreateOs(DtoOsCreator dtoOsCreator, IFormFile photo);
        Task<List<OsModel>> GetOrders(); 
    }
}
