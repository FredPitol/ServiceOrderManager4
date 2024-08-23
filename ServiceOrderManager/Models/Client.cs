namespace ServiceOrderManager.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
