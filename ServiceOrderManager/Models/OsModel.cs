namespace ServiceOrderManager.Models
{
    public class OsModel
    {
        public int ID { get; set; }
        public int ClientID { get; set; } 
        public string Name { get; set; }
        public ClientModel clientOS { get; set; } //Recebe informações do objeto cliente 
        public double ServiceCost { get; set; }
        public DateTime ExecutionDate  { get; set; } 
        public string ServiceProviderCpf { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceProviderRole { get; set; }

    }
}
