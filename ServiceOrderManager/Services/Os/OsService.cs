using ServiceOrderManager.Data;
using ServiceOrderManager.Dto;
using ServiceOrderManager.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace ServiceOrderManager.Services.Os
{
    public class OsService : IOsInterface
    {
        private readonly AppDbContext _context;

        private readonly string _system;

        public OsService(AppDbContext context, IWebHostEnvironment system)
        {
            // 9. Acesso ao banco 
            _context = context;
            _system = system.WebRootPath;

        }

        public string CreateFilePath(IFormFile photo)
        {
            var uniqueId = Guid.NewGuid().ToString();   

            var imagePathName = photo.FileName.Replace(" ", "").ToLower() + uniqueId + ".png";  // Monta image path
                                                                                                // 
            var pathToSavePhoto = _system + "\\imagem\\";    

          
            if (!Directory.Exists(pathToSavePhoto))
            {
                Directory.CreateDirectory(pathToSavePhoto);
            }


            using (var stream = File.Create(pathToSavePhoto + imagePathName))
            {
                photo.CopyToAsync(stream).Wait();
            }

            return imagePathName;

        }

        public async Task<OsModel> CreateOs(DtoOsCreator dtoOsCreator)
        {
            try
            {
                var clientTest = new ClientModel
                {
                    Name = "Teste",
                    Email = "Teste",
                    Address = dtoClientCreator.Address,
                    Cnpj = dtoClientCreator.Cnpj,
                }
                var os = new OsModel
                {
                    Name = dtoOsCreator.Name,                 
                    ServiceProviderCpf = dtoOsCreator.ServiceProviderCpf,
                    ServiceProviderName = dtoOsCreator.ServiceProviderName,
                    ServiceProviderRole = dtoOsCreator.ServiceProviderRole,
                    ExecutionDate = dtoOsCreator.ExecutionDate,
                    ServiceCost = dtoOsCreator.ServiceCost,
                    //ClientID = dtoOsCreator.ClientID,
                };

              
                _context.Add(os);
                await _context.SaveChangesAsync(); // 9.4 Espera que o processo de salvar no banco seja feito , agora podemos ir para ClientControoller -> 

                return os;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OsModel>> GetOrders()
        {
            try
            {   //10. Entra no banco e cria lista, possibilitando que o controller index acesse 
                return await _context.Os.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<OsModel> GetOsById(int id)
        {
            try
            {   
                return await _context.Os.FirstOrDefaultAsync(os => os.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OsModel> EditOs(OsModel os, IFormFile? photo)
        {
            try
            {
                var osFromDataBase = await _context.Os.AsNoTracking().SingleOrDefaultAsync(osBD => osBD.ID == os.ID); // 11.2  

                //11.3

                var imagePathName = "";

                //if (photo != null)
                //{
                //    string existingLogoPath = _system + "\\image\\" + osFromDataBase.Logo;
                //    if (File.Exists(existingLogoPath))
                //    {
                //        File.Delete(existingLogoPath);
                //    }

                //    imagePathName = CreateFilePath(photo);
                //}

             
                osFromDataBase.Name = os.Name;
                osFromDataBase.ServiceCost = os.ServiceCost;
                osFromDataBase.ExecutionDate = os.ExecutionDate;
                osFromDataBase.ServiceProviderName = os.ServiceProviderName;
                osFromDataBase.ServiceProviderRole = os.ServiceProviderRole;    
                osFromDataBase.ServiceProviderCpf = os.ServiceProviderCpf;


                //11.3 
                //if (imagePathName != "")
                //{
                //    osFromDataBase.Logo = imagePathName;
                //}
                //else
                //{
                //    clientFromDataBase.Logo = client.Logo;
                //}

                //11.4
                _context.Update(osFromDataBase);
                await _context.SaveChangesAsync();

                return os;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OsModel> RemoveOs(int id)
        {
            try
            {
                var os = await _context.Os.FirstOrDefaultAsync(osFromDataBase => osFromDataBase.ID == id);

                _context.Remove(os);
                await _context.SaveChangesAsync();

                return os;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OsModel>> GetOrdersFilter(string? search)
        {
            try
            {
                // 14
                // Adicionar task no controller caso esteja com erro
                var orders = await _context.Os.Where(osFromDataBase => osFromDataBase.Name.Contains(search)).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

