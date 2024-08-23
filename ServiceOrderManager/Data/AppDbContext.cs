using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace ServiceOrderManager.Data
{   // Herda de db context, using ctrl .
    public class AppDbContext : DbContext
    {
        // 2 Construtor conexão
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

    }
}
