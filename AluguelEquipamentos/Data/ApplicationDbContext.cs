using AluguelEquipamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelEquipamentos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
            
        }
        public DbSet<EquipamentoModel> Equipamentos { get; set; }   
    }
}
