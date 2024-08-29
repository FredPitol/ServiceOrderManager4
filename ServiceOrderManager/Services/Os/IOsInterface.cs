using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Services.Os
{
    public interface IOsInterface
    {
        Task<OsModel> CreateOs(DtoOsCreator dtoOsCreator, IFormFile photo);
        Task<List<OsModel>> GetOrders();
        Task<OsModel> GetOsById(int id); 
        Task<OsModel> EditOs(OsModel os, IFormFile? photo);
        Task<OsModel> RemoveOs(int id);
        Task<List<OsModel>> GetOrdersFilter(string? search); //14.4 Implementa no service ->
    }
}
