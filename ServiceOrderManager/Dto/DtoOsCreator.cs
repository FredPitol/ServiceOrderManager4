using ServiceOrderManager.Models;

namespace ServiceOrderManager.Dto
{
    public class DtoOsCreator
    {
        public int ID { get; set; } = 1;
        public int ClientID { get; set; } = 2;
        public string Name { get; set; } = string.Empty;
        // public ClientModel clientOS { get; set; } = new ClientModel();
        public double ServiceCost { get; set; } = 10.00;
        public DateTime ExecutionDate { get; set; } = DateTime.Now;
        public string ServiceProviderCpf { get; set; } = string.Empty ;
        public string ServiceProviderName { get; set; } = string.Empty;
        public string ServiceProviderRole { get; set; } = string.Empty; 
    }
}
