using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Models;
using System.Diagnostics.Contracts;

namespace ServiceOrderManager.Data
{   // Herda de db context, using ctrl .
    public class AppDbContext : DbContext
    {
        // 2 Construtor conexão
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        //5. Criando tabela do BD
        public DbSet<Client> Clients { get; set; }
    }
}
